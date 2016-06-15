using System;
using System.Collections;
using System.Data;
using System.Threading;
using Fanex.CustomerSetting.Module.Core.Web;

namespace Fanex.CustomerSetting.Module.SearchCopy.Services
{
    public class CheckConstraintDataForSportBook
    {
        private Hashtable _htbSporttypeBettype = new Hashtable();

        public CheckConstraintDataForSportBook()
        {
            _htbSporttypeBettype.Add("1", new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "9999" });
            _htbSporttypeBettype.Add("1_live", new string[] { "1", "3", "7", "8" });
            _htbSporttypeBettype.Add("2", new string[] { "1", "2", "3", "7", "8", "20" });
            _htbSporttypeBettype.Add("3", new string[] { "1", "2", "3", "20" });

            _htbSporttypeBettype.Add("5", new string[] { "1", "2", "3", "20", "9999" });

            _htbSporttypeBettype.Add("8", new string[] { "1", "3", "20" });
            _htbSporttypeBettype.Add("10", new string[] { "1", "2", "3", "20" });
            _htbSporttypeBettype.Add("11", new string[] { "1", "20" });
            _htbSporttypeBettype.Add("99", new string[] { "1", "2", "3", "9", "20" });
            _htbSporttypeBettype.Add("154", new string[] { "20" });
            _htbSporttypeBettype.Add("161", new string[] { "81", "83", "86", "88" });
            _htbSporttypeBettype.Add("161_live", new string[] { "85", "86", "87", "89", "90" });
        }

        public bool IsChangedData(DataRow dr)
        {
            string parameter = string.Empty;
            string parameter_max = string.Empty;
            foreach (string key in _htbSporttypeBettype.Keys)
            {
                string[] bettypes = (string[])_htbSporttypeBettype[key];
                switch (key)
                {
                    case "1":
                        parameter = "apositiontaking_{0}";
                        parameter_max = "maxpos_{0}";
                        break;
                    case "1_live":
                        parameter = "alivepositiontaking_{0}";
                        parameter_max = "maxlpos_{0}";
                        break;
                    case "161_live":
                        parameter = "v161alivepositiontaking_{0}";
                        parameter_max = "maxv161lpos_{0}";
                        break;
                    default:
                        parameter = string.Format("v{0}apositiontaking_", key) + "{0}";
                        parameter_max = string.Format("maxv{0}pos_", key) + "{0}";
                        break;
                }

                foreach (string bettype in bettypes)
                {
                    string param = string.Format(parameter, bettype);
                    string param_max = string.Format(parameter_max, bettype);
                    if (Convert.ToDouble(dr[param]) > Convert.ToDouble(dr[param_max]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void GenerateInfoAccount(DataRow dr, int custId, int uplineId, int userId, int subAccId, ref Hashtable htbParameters)
        {
            htbParameters.Add("custid", 0);
            htbParameters.Add("firstname", string.Empty);
            htbParameters.Add("lastname", string.Empty);
            htbParameters.Add("phone", string.Empty);
            htbParameters.Add("mobilephone", string.Empty);
            htbParameters.Add("credit", 0);
            htbParameters.Add("username", string.Empty);
            htbParameters.Add("userpwd", string.Empty);
            htbParameters.Add("fax", string.Empty);
            htbParameters.Add("playerdiscount", dr["playerdiscount"]);
            htbParameters.Add("playerdiscountcs", dr["playerdiscountcs"]);
            htbParameters.Add("playerdiscount1x2", dr["playerdiscount1x2"]);
            htbParameters.Add("playerdiscountnumber", dr["playerdiscountnumber"]);
            htbParameters.Add("playerdiscounthrfixedodds", dr["playerdiscounthrfixedodds"]);
            htbParameters.Add("playerdiscountmyanmarodds", dr["playerdiscountmyanmarodds"]);
            htbParameters.Add("suspended", dr["suspended"]);
            htbParameters.Add("recommend", uplineId);
            htbParameters.Add("ugroup", dr["ugroup"]);
            htbParameters.Add("closed", dr["closed"]);
            htbParameters.Add("issearchandcopy", 1);
            htbParameters.Add("disablenumber", dr["disablenumber"]);
            htbParameters.Add("disablehrfixedodds", dr["DisableHRFixedOdds"]);
            htbParameters.Add("language", Thread.CurrentThread.CurrentCulture.Name);
            htbParameters.Add("ip", CurrentContext.Instance.Ip4Address);
            htbParameters.Add("userid", userId);
            htbParameters.Add("creator", userId);
            htbParameters.Add("subaccid", subAccId);
            htbParameters.Add("errcode", 0);
            htbParameters.Add("errmsg", string.Empty);
        }

        public void GeneratePositionTaking(DataRow dr, ref Hashtable htbParameters)
        {
            string parameter = string.Empty;
            string parameter_max = string.Empty;
            foreach (string key in _htbSporttypeBettype.Keys)
            {
                string[] bettypes = (string[])_htbSporttypeBettype[key];
                switch (key)
                {
                    case "1":
                        parameter = "apositiontaking_{0}";
                        parameter_max = "maxpos_{0}";
                        break;
                    case "1_live":
                        parameter = "alivepositiontaking_{0}";
                        parameter_max = "maxlpos_{0}";
                        break;
                    case "161_live":
                        parameter = "v161alivepositiontaking_{0}";
                        parameter_max = "maxv161lpos_{0}";
                        break;
                    default:
                        parameter = string.Format("v{0}apositiontaking_", key) + "{0}";
                        parameter_max = string.Format("maxv{0}pos_", key) + "{0}";
                        break;
                }

                foreach (string bettype in bettypes)
                {
                    string param = string.Format(parameter, bettype);
                    string param_max = string.Format(parameter_max, bettype);

                    double min = Math.Min(Convert.ToDouble(dr[param]), Convert.ToDouble(dr[param_max]));
                    htbParameters.Add(param, min);
                }
            }
        }

        public void GenerateBettingLimit(DataRow dr, ref Hashtable htbParameters)
        {
            string[] sportTypes = new string[] { "1", "2", "3", "5", "8", "10", "11", "99", "99mp", "154", "161" };
            string[] parameters = new string[] 
                                    {
                                        "minbetv{0}",
                                        "maxbetv{0}",
                                        "maxpermatchv{0}"
                                    };

            foreach (string item in sportTypes)
            {
                if (item == "1")
                {
                    htbParameters.Add("minbet", dr["minbet"]);
                    htbParameters.Add("maxbet", dr["maxbet"]);
                    htbParameters.Add("maxpermatch", dr["maxpermatch"]);
                }
                else
                {
                    if (item == "161")
                    {
                        htbParameters.Add("maxperballv161", dr["maxperballv161"]);
                    }

                    foreach (string param in parameters)
                    {
                        string formatParam = string.Format(param, item);
                        if (!htbParameters.ContainsKey(formatParam))
                        {
                            htbParameters.Add(formatParam, dr[formatParam]);
                        }
                    }
                }
            }
        }
    }
}