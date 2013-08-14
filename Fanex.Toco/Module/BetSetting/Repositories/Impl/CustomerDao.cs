// -----------------------------------------------------------------------
// <copyright file="CustomerDao.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.BetSetting.Repositories.Impl
{
    using System.Collections;
    using Fanex.Common.Extensions;
    using Fanex.CustomerSetting.Module.BetSetting.Repositories.Interface;
    using Fanex.CustomerSetting.Module.BetSetting.ViewModels;
    using Fanex.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CustomerDao : ICustomerDao
    {
        public Customer UserInfo(int custId)
        {
            var param = new Hashtable();
            param.Add("custid", custId);

            using (var objDb = new ObjectDb("Age_CustomerSelUserInfo"))
            {
                using (var reader =  objDb.ExecuteReader(param))
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            return new Customer
                            {
                                CustId = reader.ToInt("custid"),
                                Username = reader.ToString("username"),
                                FirstName = reader.ToString("firstname"),
                                LastName = reader.ToString("lastname"),
                                MobilePhone = reader.ToString("mobilephone"),
                                HomePhone = reader.ToString("phone"),
                                Email = reader.ToString("email"),
                                Fax = reader.ToString("fax"),
                                RoleId = reader.ToInt("roleid")
                            };
                        }
                    }

                    return null;   
                }                
            }
        }

        public int UserGroupId(int custId)
        {
            var param = new Hashtable();
            param.Add("custid", custId);

            using (var objDb = new ObjectDb("Acc_Con_FileNameSelByCustID"))
            {
                using (var reader = objDb.ExecuteReader(param))
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            return reader.ToInt("groupid");
                        }
                    }

                    return 0;
                }
            }
        }
    }
}
