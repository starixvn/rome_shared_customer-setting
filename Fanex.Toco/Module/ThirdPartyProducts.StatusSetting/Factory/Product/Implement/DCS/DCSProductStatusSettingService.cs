using Fanex.Data;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria;
using ThirdPartyProducts.StatusSetting.Factory.Product.Criteria.DCS;
using ThirdPartyProducts.StatusSetting.Model;
using ThirdPartyProducts.StatusSetting.Resources;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Implement.DCS
{
    internal class DCSProductStatusSettingService : ProductStatusSettingServiceBase
    {
        public override List<ProductStatus> Select(int custId, int productId, int roleId)
        {
            var criteria = new SelectDCSProductStatusCriteria
            {
                CustId = custId,
                ProductId = productId,
                RoleId = roleId
            };

            var result = new List<ProductStatus>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.AssignParamValues(criteria.GetSpParams());
                var reader = db.ExecuteReader();
                while (reader.Read())
                {
                    var producStatus = new ProductStatus
                    {
                        ProductId = Utils.ToInt32(reader["ProductID"]),
                        BetType = Utils.ToInt32(reader["Bettype"]),
                        SportType = Utils.ToInt32(reader["Sporttype"]),
                        IsDisabled = Utils.ToBoolean(reader["IsDisabled"]),
                        IsDisabledUpline = Utils.ToBoolean(reader["IsDisabledUpline"]),
                        IsDisabledDownline = Utils.ToBoolean(reader["IsDisabledDownline"]),
                        HasDownline = Utils.ToBoolean(reader["HasDownline"]),
                        IsSync = Utils.ToBoolean(reader["IsSync"]),
                        IsSyncedUpline = Utils.ToBoolean(reader["IsSyncedUpline"]),
                        IsSyncedDownline = Utils.ToBoolean(reader["IsSyncedDownline"]),
                        IsHidden = Utils.ToBoolean(reader["IsHidden"]),
                        IsHiddenUpline = Utils.ToBoolean(reader["IsHiddenUpline"])
                    };

                    producStatus.GetNameFromRemotingRef();
                    result.Add(producStatus);
                }

                return result;
            }
        }

        public override List<ProductStatusResult> Update(
            int custId,
            int subAccId,
            int uplineId,
            int productId,
            int userId,
            int systemId,
            string ip,
            List<ProductStatus> productStatus)
        {
            var result = new List<ProductStatusResult>();
            Validate(custId, subAccId, productId, userId, systemId, ip, productStatus);
            var criteria = new UpdateDCSProductStatusCriteria
            {
                CustId = custId,
                SubAccId = subAccId,
                UserId = userId,
                Ip = ip,
                SystemId = systemId
            };

            criteria.MapSpParams(productStatus);
            result.Add(ExecuteNonQuery(criteria));
            return result;
        }

        internal ProductStatusResult ConvertXmlErrorMessage(ProductStatusResult productStatusResultWithXmlErrorMessage, bool showBettypes)
        {
            var result = new ProductStatusResult
            {
                ErrorCode = productStatusResultWithXmlErrorMessage.ErrorCode,
                ErrorMessage = productStatusResultWithXmlErrorMessage.ErrorMessage
            };
            var xmlErrMsg = productStatusResultWithXmlErrorMessage.ErrorMessage;
            if (!string.IsNullOrEmpty(xmlErrMsg) && xmlErrMsg.StartsWith("<"))
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlErrMsg);
                var errCode = xmlDoc.FirstChild.Attributes["MsgID"].Value;
                var newErrorMessage = new StringBuilder(DCSMessage.ResourceManager.GetString("Msg" + errCode));
                var language = Utils.GetLanguageKeyForRemotingRef();
                var childNodes = xmlDoc.FirstChild.ChildNodes;
                if (showBettypes)
                {
                    foreach (XmlNode node in childNodes)
                    {
                        string bettypeName = Utils.GetBetTypeName(node.Attributes["BetType"].Value, language);
                        newErrorMessage.Append("\r\n\t");
                        newErrorMessage.Append(bettypeName);
                    }
                }

                return new ProductStatusResult
                {
                    ErrorMessage = newErrorMessage.ToString(),
                    ErrorCode = errCode
                };
            }

            return result;
        }
    }
}
