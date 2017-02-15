using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TreeProcessing.NET
{
    public class AttributeJsonConverter : JsonConverter
    {
        private static IDictionary<NodeType, Type> nodeTypes = new Dictionary<NodeType, Type>();
        private const string PropertyName = "Type";

        static AttributeJsonConverter()
        {
            KeyValuePair<NodeType, Type>[] keyValuePairs =
                typeof(Node).GetTypeInfo().Assembly.DefinedTypes
                .Where(t => t.IsSubclassOf(typeof(Node)) && !t.IsAbstract && t.GetCustomAttribute<NodeAttr>() != null)
                .Select(t => new KeyValuePair<NodeType, Type>(t.GetCustomAttribute<NodeAttr>().NodeType, t.AsType()))
                .ToArray();

            foreach (var keyValuePair in keyValuePairs)
            {
                nodeTypes[keyValuePair.Key] = keyValuePair.Value;
            }
        }

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
                    var type = Type.GetType(typeof(Node).Namespace + "." + obj.ToString());
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
            NodeAttr nodeAttr = type.GetTypeInfo().GetCustomAttribute<NodeAttr>();
            jObject.Add(PropertyName, nodeAttr.NodeType.ToString());
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
