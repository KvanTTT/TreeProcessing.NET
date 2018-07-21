using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace TreeProcessing.NET
{
    public class PropertyJsonConverter : JsonConverter
    {
        private static IDictionary<NodeType, Type> nodeTypes = new Dictionary<NodeType, Type>();

        static PropertyJsonConverter()
        {
            KeyValuePair<NodeType, Type>[] keyValuePairs =
                typeof(Node).GetTypeInfo().Assembly.DefinedTypes
                .Where(t => t.IsSubclassOf(typeof(Node)) && !t.IsAbstract)
                .Select(t => new KeyValuePair<NodeType, Type>((NodeType)Enum.Parse(typeof(NodeType), t.Name), t.AsType()))
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
                    var nodeTypeObject = jObject[nameof(NodeType)];
                    var nodeType = (NodeType)Enum.Parse(typeof(NodeType), nodeTypeObject.ToString());
                    target = Activator.CreateInstance(nodeTypes[nodeType]);
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
            throw new NotSupportedException();
        }
    }
}
