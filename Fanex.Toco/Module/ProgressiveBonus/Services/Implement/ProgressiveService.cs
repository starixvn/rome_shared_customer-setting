// -----------------------------------------------------------------------
// <copyright file="ProgressiveService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <summary>
//  ProgressiveService
// </summary>

namespace Fanex.CustomerSetting.Module.ProgressiveBonus.Services.Implement
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using CasinoLibrary.SBAgent;
	using Fanex.CustomerSetting.Module.Core;
	using Fanex.CustomerSetting.Module.Core.Models;
	using Fanex.CustomerSetting.Module.Core.Services.Impl;
	using Fanex.CustomerSetting.Module.Core.Web;
	using Fanex.CustomerSetting.Module.ProgressiveBonus.Configurations;
	using Fanex.CustomerSetting.Module.ProgressiveBonus.Criteria;
	using Fanex.CustomerSetting.Module.ProgressiveBonus.Entities;
	using Fanex.CustomerSetting.Module.ProgressiveBonus.Permission;
	using Fanex.CustomerSetting.Module.ProgressiveBonus.Repositories.Impl;
	using Fanex.CustomerSetting.Module.ProgressiveBonus.Repositories.Interface;
	using Fanex.CustomerSetting.Module.ProgressiveBonus.Resources;
	using Fanex.CustomerSetting.Module.ProgressiveBonus.Services.Interface;
	using Fanex.NAP.Framework.Data;

	/// <summary>
	/// Progressive Service
	/// </summary>
	public class ProgressiveService : IProgressiveService
	{
		private readonly IProgressiveRepository _progressiveRepository;
		private readonly FromSystem _fromSystem = (FromSystem)ProgressiveSection.Instance.System;
		
		public ProgressiveService()
			: this(null)
		{
		}

		/// <summary>
		/// Check user permission when getting Progressive Bonus
		/// </summary>
		private bool EnabledProgressiveBonus
		{
			get
			{
				return UserPermission.CheckProgressiveBonusPermission();
			}
		}

		/// <summary>
		/// Check user permission when updating Progressive Bonus
		/// </summary>
		private bool EnabledProgressiveBonusWithRole
		{
			get
			{
				return UserPermission.CheckProgressiveBonusPermissionWithRole();
			}
		}

		public ProgressiveService(IProgressiveRepository progressiveRepository)
		{
			this._progressiveRepository = progressiveRepository ?? new ProgressiveRepository();
		}

		/// <summary>
		/// Get Progressive Bonus
		/// </summary>
		/// <param name="custId">Customer Id</param>
		/// <returns>Progressive object</returns>
		public Progressive GetProducts(int custId)
		{
			if (!this.EnabledProgressiveBonus)
			{
				return null;
			}

			Progressive progressive = null;
			List<Product> listProducts = new List<Product>();
			try
			{
                GetProductCriteria criteria = null;
                criteria = new GetProductCriteria
                {
                    CustId = custId
                };

				// Get status from Db
                ProgressiveStatus status = this._progressiveRepository.GetStatusProgressiveBonus(criteria);

				progressive = new Progressive
				{
					CustId = custId,
					UserName = status.Username,
					RoleId = status.RoleId,
					Products = listProducts
				};

				var products = ProgressiveSection.Instance.Product;
				foreach (ProgressiveElement product in products)
				{
					if (product.Active && product.Id == 72)
					{
						string productName = Resources.Language.ResourceManager.GetString(product.Name);
                        var p = new Product()
                        {
                            Id = product.Id,
							Name = string.IsNullOrEmpty(productName) ? product.Name : productName
                        };

                        if (status.DisableCasinoBonus)//1:Disable
                        {
                            p.Enable = false;
                            p.EnableButton = true;

                            p.EnableDownlines = false;
                            p.EnableDownlinesButton = false;    
                        }
                        else//0: Enable
                        {
                            p.Enable = true;
                            p.EnableButton = true;

                            p.EnableDownlines = !status.DisableDownlinesCasinoBonus;
                            p.EnableDownlinesButton = false;
                        }

						listProducts.Add(p);
					}
				}
			}
			catch (Exception ex)
			{
				LoggingHandler.Log(ex);
			}

			return progressive;
		}

		/// <summary>
		/// Update Progressive Bonus
		/// </summary>
		/// <param name="progressive">Progressive object</param>
		/// <returns>Reuslt code of server</returns>
		public List<Result> UpdateProgressive(Progressive progressive)
		{
			if (!this.EnabledProgressiveBonusWithRole)
			{
				return null;
			}

			List<Result> results = new List<Result>();
			Hashtable outputParams = null;
			try
			{
				string ip = Utils.GetClientIP();
				string language = "E";

				int userId = AppSession.AdminId;
				int subAccId = this._fromSystem == FromSystem.Accounting // Acc
					? userId
					: AppSession.SubAccountId;

				foreach (Product product in progressive.Products)
				{
					if (product.Id == 72) //Progressive Bonus
					{
						try
						{
							// 1. call function in CasinoLibrary first
							bool result = this.UpdateCustEnableBonusCasinoLibrary(progressive.CustId,
								product.Enable,
								product.EnableDownlines,
								ip,
								userId,
								progressive.AdminUserName,
								(int)this._fromSystem);
							if (result)
							{
								ICriteria criteria = new UpdateProgressiveCriteria
								{
									CustId = progressive.CustId,
									Disable = !product.Enable,
									DisableDownlines = !product.EnableDownlines,
									UserId = userId,
									SubAccId = subAccId,
									IP = ip,
									Language = language
								};
								// 2. update ProgressiveBonus
								outputParams = this._progressiveRepository.UpdateStatusProgressiveBonus(criteria);
								if (outputParams != null)
								{
									int errCode = Convert.ToInt32(outputParams["errcode"]);
									results.Add(new Result
									{
										ErrorCode = errCode,
										ErrorMessage = errCode == 0 ? "Progessive Bonus updated successfully" : outputParams["errmsg"].ToString()
									});
								}
							}
							else // update fail in CasinoLibrary
							{
								results.Add(new Result
								{
									ErrorCode = -1,
									ErrorMessage = Language.FailUpdatingProgressiveBonusInAPI
								});
							}
						}
						catch (Exception ex)
						{
							LoggingHandler.Log(ex);

							results.Add(new Result()
							{
								ErrorCode = product.Id,
								ErrorMessage = ex.Message
							});
						}
					}
				}
			}
			catch (Exception ex)
			{
				LoggingHandler.Log(ex);

				results.Add(new Result()
				{
					ErrorCode = 100,
					ErrorMessage = ex.Message
				});
			}

			return results;
		}

		
		private bool UpdateCustEnableBonusCasinoLibrary(int custId,
			bool enable,
			bool enableDownlines,
			string ip,
			int modifiedBy,
			string modifiedUsername,
			int fromSystem)
		{
			Customer customerService = new Customer();
			return customerService.UpdateCustEnableBonus(custId,
				enable,
				enableDownlines,
				ip,
				modifiedBy,
				modifiedUsername,
				fromSystem);
		}
	}
}