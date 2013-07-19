using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fanex.CustomerSetting.Module.ProgressiveModule.Services.Interface;
using Fanex.CustomerSetting.Module.ProgressiveModule.Entities;
using Fanex.CustomerSetting.Module.Core.Services.Impl;
using Fanex.CustomerSetting.Module.ProgressiveModule.Configurations;
using Fanex.CustomerSetting.Module.Core.Models;

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Services.Implement
{
    public class ProgressiveService : IProgressiveService
    {
        public Progressive GetProducts()
        {
            Progressive progressive = new Progressive();
            progressive.CustId = 1234;
            progressive.UserName = "IT-IBC";
            progressive.RoleId = 4;

            List<Product> listProducts = new List<Product>();          

            try
            {
                var products = ProgressiveSection.Instance.Product;
                foreach (ProgressiveElement product in products)
                {
                    if (product.Active)
                    {
                        listProducts.Add(new Product() 
                        { 
                            Id = product.Id,
                            Name = product.Name,
                            Enable = true,
                            EnableDownlines = false
                        });
                    }
                }

               
            }
            catch (Exception ex)
            {
                LoggingHandler.Log(ex);
            }

            progressive.Products = listProducts;

            return progressive;
        }


        public List<Result> UpdateProgressive(Progressive progressive, string ip, int userId, string userName, int fromSystem)
        {
            List<Result> results = new List<Result>();
            try
            {
                foreach (Product product in progressive.Products)
                {
                    if (product.Id == 72)//Progressive Bonus
                    {
                        try
                        {
                            var casinoLibray = new CasinoLibrary.SBAgent.Customer();
                            bool rs = casinoLibray.UpdateCustEnableBonus(progressive.CustId, product.Enable, product.EnableDownlines, ip, userId, userName, fromSystem);
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
    }
}