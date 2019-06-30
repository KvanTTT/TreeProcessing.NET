using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace TreeProcessing.NET
{
    public class ClassNameJsonConverter : JsonConverter
    {
        private const string PropertyName = "Type";

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Node) || objectType.GetTypeInfo().IsSubclassOf(typeof(Node));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Null)
            {
                JObject jObject = JObject.Load(reader);

                object target = null;
                if (objectType == typeof(Node) || objectType.GetTypeInfo().IsSubclassOf(typeof(Node)))
                {
                    var obj = jObject[PropertyName];
                    var type = Type.GetType(typeof(Node).Namespace + "." + obj);
                    target = Activator.CreateInstance(type);
                }
                else
                {
                    throw new FormatException("Invalid JSON");
                }

                serializer.Populate(jObject.CreateReader(), target);
                return target;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject jObject = new JObject();
            Type type = value.GetType();
            jObject.Add(PropertyName, type.Name);
            PropertyInfo[] properties = ReflectionCache.GetClassProperties(type);
            foreach (PropertyInfo prop in properties)
            {
                object propVal = prop.GetValue(value, null);
                if (propVal != null)
                {
                    jObject.Add(prop.Name, JToken.FromObject(propVal, serializer));
                }
            }
            jObject.WriteTo(writer);
        }
    }
}
