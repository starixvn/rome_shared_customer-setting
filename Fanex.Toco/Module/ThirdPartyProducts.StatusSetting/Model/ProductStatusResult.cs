using RemotingRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ThirdPartyProducts.StatusSetting.Resources;

namespace ThirdPartyProducts.StatusSetting.Model
{
    public class ProductStatusResult
    {
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        internal void ConvertXMLErrorMessage()
        {
            var xmlErrMsg = ErrorMessage;
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlErrMsg);
            ErrorCode = xmlDoc.FirstChild.Attributes["MsgID"].Value;
            var newErrorMessage = new StringBuilder(DCSMessage.ResourceManager.GetString("Msg" + ErrorCode));
            var language = Utils.GetLanguageKeyForRemotingRef();
            foreach (XmlNode node in xmlDoc.FirstChild.ChildNodes)
            {
                string bettypeName = GetBetTypeName(Convert.ToInt32(node.Attributes["BetType"].Value), language);
                newErrorMessage.Append("\r\n\t");
                newErrorMessage.Append(bettypeName);
            }

            ErrorMessage = newErrorMessage.ToString();
        }

        private string GetBetTypeName(int bettype, string language)
        {
            var result = Resources.BetTypeName.ResourceManager.GetString("BetType_" + bettype);
            if (string.IsNullOrEmpty(result))
            {
                result = RefData.GetBetTypeName(bettype.ToString(), language, string.Empty);
            }

            return result;
        }
    }
}
