// ************************************************************************************
// <copyright file="SerializeHelper.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="steven.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Fanex.CustomerSetting.Module.SearchCopy.Helper
{
    /// <summary>
    /// Class SerializeHelper.
    /// </summary>
    public class SerializeHelper
    {
        /// <summary>
        /// Gets the JSON serializer setting.
        /// </summary>
        /// <returns>Return JSON format.</returns>
        public static JsonSerializerSettings GetJsonSerializerSetting()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return settings;
        }

        /// <summary>
        /// Deserialize the JSON string.
        /// </summary>
        /// <typeparam name="T">Specific object.</typeparam>
        /// <param name="serializedObject">The customer string.</param>
        /// <returns>Return specific object.</returns>
        public static T DeserializeJsonString<T>(string serializedObject)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            T customer =
                 JsonConvert.DeserializeObject<T>(serializedObject, settings);
            return customer;
        }
    }
}