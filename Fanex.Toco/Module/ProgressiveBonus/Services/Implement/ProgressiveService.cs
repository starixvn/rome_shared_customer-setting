using System;
using System.Collections;
using System.Collections.Generic;
using Fanex.Common;
using Fanex.CustomerSetting.Module.Core;
using Fanex.CustomerSetting.Module.Core.Models;
using Fanex.CustomerSetting.Module.Core.Services.Impl;
using Fanex.CustomerSetting.Module.Core.Web;
using Fanex.CustomerSetting.Module.ProgressiveModule.Configurations;
using Fanex.CustomerSetting.Module.ProgressiveModule.Criteria;
using Fanex.CustomerSetting.Module.ProgressiveModule.Entities;
using Fanex.CustomerSetting.Module.ProgressiveModule.Repositories.Impl;
using Fanex.CustomerSetting.Module.ProgressiveModule.Repositories.Interface;
using Fanex.CustomerSetting.Module.ProgressiveModule.Services.Interface;
using Fanex.NAP.Framework.Data;

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Services.Implement
{
	public class ProgressiveService : IProgressiveService
	{
		private readonly IProgressiveRepository _progressiveRepository;

		private readonly FromSystem _fromSystem = (FromSystem)ProgressiveSection.Instance.System;
		private readonly bool _enableProgressiveBonus = ProgressiveSection.Instance.Active;

		public ProgressiveService()
			: this(null)
		{ }

		public ProgressiveService(IProgressiveRepository progressiveRepository)
		{
			_progressiveRepository = progressiveRepository ?? new ProgressiveRepository();
		}

		public Progressive GetProducts()
		{
			if (!_enableProgressiveBonus)
			{
				return null;
			}

			Progressive progressive = null;
			List<Product> listProducts = new List<Product>();
			try
			{
				int adminId, requestRoleId;
				string adminUsername, requestUsername;

				adminId = AppSession.AdminId;
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
				dynamic rawData = this._progressiveRepository.GetStatusOfProgressiveBonus(criteria);

				progressive = new Progressive
				{
					AdminId = adminId,
					AdminUserName = adminUsername,
					CustId = Converter.ToInt(rawData.custid),
					UserName = requestUsername,
					RoleId = requestRoleId,
					Products = listProducts
				};

				var products = ProgressiveSection.Instance.Product;
				foreach (ProgressiveElement product in products)
				{
					if (product.Active && product.Id == 72)
					{
						listProducts.Add(new Product()
						{
							Id = product.Id,
							Name = product.Name,
							Enable = !Converter.ToBool(rawData.DisableCasinoBonus),
							EnableDownlines = false
						});
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
			if (!_enableProgressiveBonus)
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
			if (!_enableProgressiveBonus)
			{
				return false;
			}

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