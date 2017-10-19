using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        public override List<ProductStatus> Select(GetProductStatusRequest request)
        {
            var criteria = new SelectDCSProductStatusCriteria
            {
                CustId = request.CustId,
                ProductId = request.ProductId,
                RoleId = request.RoleId,
                IsDeposit = request.IsDeposit
            };

            return ObjectDbFactory.ExecuteReaderFunc(criteria, MapSpData).ToList();
        }

        public override List<ProductStatusResult> Update(UpdateProductStatusRequest request)
        {
            var result = new List<ProductStatusResult>();
            Validate(request);
            var criteria = new UpdateDCSProductStatusCriteria
            {
                CustId = request.CustId,
                SubAccId = request.SubAccId,
                UserId = request.UserId,
                Ip = request.Ip,
                SystemId = request.SystemId,
                IsDeposit = request.IsDeposit
            };

            criteria.MapSpParams(request.ProductStatus);
            result.Add(ExecuteNonQuery(criteria));
            return result;
        }

        protected ProductStatusResult ConvertXmlErrorMessage(ProductStatusResult productStatusResultWithXmlErrorMessage, bool showBettypes)
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
                if (showBettypes)
                {
                    foreach (XmlNode node in xmlDoc.FirstChild.ChildNodes)
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
