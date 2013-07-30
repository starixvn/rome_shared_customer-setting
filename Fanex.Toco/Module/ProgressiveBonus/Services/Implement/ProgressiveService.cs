using System;
using System.Collections;
using System.Collections.Generic;
using Fanex.Common;
using Fanex.CustomerSetting.Module.ProgressiveModule.Configurations;
using Fanex.CustomerSetting.Module.ProgressiveModule.Criteria;
using Fanex.CustomerSetting.Module.ProgressiveModule.Entities;
using Fanex.CustomerSetting.Module.ProgressiveModule.Permission;
using Fanex.CustomerSetting.Module.ProgressiveModule.Repositories.Impl;
using Fanex.CustomerSetting.Module.ProgressiveModule.Repositories.Interface;
using Fanex.CustomerSetting.Module.ProgressiveModule.Services.Interface;
using Fanex.NAP.Framework.Data;
using Fanex.CustomerSetting.Module.Core.Models;
using Fanex.CustomerSetting.Module.Core.Web;
using Fanex.CustomerSetting.Module.Core.Services.Impl;
using Fanex.CustomerSetting.Module.Core;

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Services.Implement
{
	public class ProgressiveService : IProgressiveService
	{
		private readonly IProgressiveRepository _progressiveRepository;
		private readonly FromSystem _fromSystem = (FromSystem)ProgressiveSection.Instance.System;
		private bool EnabledProgressiveBonus
		{
			get
			{
				return UserPermission.CheckProgressiveBonusPermission();
			}
		}

		private bool EnabledProgressiveBonusWithRoles
		{
			get
			{
				return UserPermission.CheckProgressiveBonusPermissionWithRoles();
			}
		}

		public ProgressiveService()
			: this(null)
		{ }

		public ProgressiveService(IProgressiveRepository progressiveRepository)
		{
			_progressiveRepository = progressiveRepository ?? new ProgressiveRepository();
		}

		public Progressive GetProducts()
		{
			if (!EnabledProgressiveBonus)
			{
				return null;
			}

			Progressive progressive = null;
			List<Product> listProducts = new List<Product>();
			try
			{
				int adminId, adminRoleId, requestRoleId;
				string adminUsername, requestUsername;

				adminId = AppSession.AdminId;
				adminRoleId = AppSession.AdminRoleId;
				adminUsername = AppSession.AdminUsername;

				requestUsername = AppSession.RequestUsername;
				requestRoleId = AppSession.RequestRoleId;

				ICriteria criteria = null;
				if (_fromSystem == FromSystem.Accounting) // Acc
				{
					criteria = new GetAccProductCriteria
					{
						Username = requestUsername
					};
				}
				else // Age
				{
					criteria = new GetAgeProductCriteria
					{
						Username = requestUsername,
						UplineId = adminId,
						IsInternalAgent = AppSession.IsInternalAgent
					};
				}

				// Get status from Db
                ProgressiveStatus status = this._progressiveRepository.GetStatusOfProgressiveBonus(criteria);

				progressive = new Progressive
				{
					AdminId = adminId,
					AdminUserName = adminUsername,
					AdminRoleId = adminRoleId,
					CustId = Converter.ToInt(status.CustId),
					UserName = requestUsername,
					RoleId = requestRoleId,
					Products = listProducts
				};

				var products = ProgressiveSection.Instance.Product;
				foreach (ProgressiveElement product in products)
				{
					if (product.Active && product.Id == 72)
					{
                        var p = new Product()
                        {
                            Id = product.Id,
                            Name = product.Name,
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

		public List<Result> UpdateProgressive(Progressive progressive)
		{
			if (!EnabledProgressiveBonusWithRoles)
			{
				return null;
			}

			List<Result> results = new List<Result>();
			Hashtable outputParams = null;
			try
			{
				string ip = Utils.GetClientIP();
				string language = AppSession.Language;

				int userId = AppSession.AdminId;
				int subAccId = _fromSystem == FromSystem.Accounting // Acc
					? userId
					: AppSession.SubAccountId;

				foreach (Product product in progressive.Products)
				{
					if (product.Id == 72) //Progressive Bonus
					{
						try
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
							outputParams = this._progressiveRepository.UpdateStatusOfProgressiveBonus(criteria);
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

		public bool IsDisableProgessiveBonus(string username, int uplineId, bool isInternal)
		{
			ICriteria criteria = null;
			if (_fromSystem == FromSystem.Accounting) // Acc
			{
				criteria = new GetAccProductCriteria
				{
					Username = username
				};
			}
			else // Age
			{
				criteria = new GetAgeProductCriteria
				{
					Username = username,
					UplineId = uplineId,
					IsInternalAgent = isInternal
				};
			}

			var rawData = this._progressiveRepository.GetStatusOfProgressiveBonus(criteria);
			if (rawData != null)
			{
				return Converter.ToBool(rawData.DisableCasinoBonus);
			}

			return false;
		}
	}
}