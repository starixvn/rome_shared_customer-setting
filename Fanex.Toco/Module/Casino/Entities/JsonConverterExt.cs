namespace Fanex.CustomerSetting.Module.Casino.Entities
{
    using System;
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Core;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class JsonConverterExt : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(KeyValuePair<string, int>).IsAssignableFrom(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            serializer.ConstructorHandling = ConstructorHandling.Default;
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, JsonSerializer serializer)
        {
            KeyValuePair<string, int> result;

            if (reader.TokenType == JsonToken.StartObject)
            {
                JToken token = JArray.ReadFrom(reader);

                result = new KeyValuePair<string, int>(Utils.ToString(token["key"]), Utils.ToInt32(token["value"]));
            }
            else
            {
                result =
                    (KeyValuePair<string, int>)
                        serializer.Deserialize(reader, typeof(KeyValuePair<string, int>));
            }

            return result;
        }

        /*public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            KeyValuePair<string, int> result;

            if (reader.TokenType == JsonToken.StartObject)
            {
                JToken token = JArray.ReadFrom(reader);

                result = new KeyValuePair<string, int>(Utils.ToString(token["key"]), Utils.ToInt32(token["value"]));
            }
            else
            {
                result =
                    (KeyValuePair<string, int>)
                        serializer.Deserialize(reader, typeof(KeyValuePair<string, int>));
            }

            return result;
        }*/
    }
}