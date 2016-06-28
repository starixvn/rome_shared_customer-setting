// ************************************************************************************
// <copyright file="SerializeHelper.cs" company="">
//     Copyright (c). All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="steven.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Helpers
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Class SerializeHelper.
    /// </summary>
    public sealed class SerializeHelper
    {
        private SerializeHelper()
        { 
        }

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