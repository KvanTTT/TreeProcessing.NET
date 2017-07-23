using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Xunit;

namespace TreeProcessing.NET.Tests
{
    public class SerializationTests
    {
#if PORTABLE || NET
        [Theory]
        [InlineData(TestHelper.Platform)]
        public void Binary_Serialization(string platform)
        {
            Statement tree = SampleTree.Init();

            Statement actualTree;
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (var memoryStream = new System.IO.MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, tree);

                memoryStream.Position = 0;
                var chars = new byte[memoryStream.Length];
                memoryStream.Read(chars, 0, (int)memoryStream.Length);
                var str = Encoding.UTF8.GetString(chars);

                memoryStream.Position = 0;
                actualTree = (Statement)binaryFormatter.Deserialize(memoryStream);
            }

            Assert.Equal(0, tree.CompareTo(actualTree));
        }
#endif

        [Theory]
        [InlineData(TestHelper.Platform)]
        public void DataContract_Serialization(string platform)
        {
            Statement tree = SampleTree.Init();

            Statement actualTree;
            DataContractSerializer serializer = new DataContractSerializer(typeof(Statement));
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = System.Xml.XmlDictionaryWriter.CreateTextWriter(memoryStream, Encoding.UTF8))
                {
                    serializer.WriteObject(memoryStream, tree);

                    memoryStream.Position = 0;
                    var chars = new byte[memoryStream.Length];
                    memoryStream.Read(chars, 0, (int)memoryStream.Length);
                    var str = Encoding.UTF8.GetString(chars);

                    memoryStream.Position = 0;
                    actualTree = (Statement)serializer.ReadObject(memoryStream);
                }
            }

            Assert.Equal(0, tree.CompareTo(actualTree));
        }

        [Theory]
        [InlineData(TestHelper.Platform)]
        public void Xml_Serialization(string platform)
        {
            Statement tree = SampleTree.Init();
            XmlSerializer serializer = new XmlSerializer(typeof(Statement));

            Statement actualTree;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                serializer.Serialize(memoryStream, tree);

                memoryStream.Position = 0;
                var chars = new byte[memoryStream.Length];
                memoryStream.Read(chars, 0, (int)memoryStream.Length);
                var str = Encoding.UTF8.GetString(chars);

                memoryStream.Position = 0;
                actualTree = (Statement)serializer.Deserialize(memoryStream);
            }

            Assert.Equal(0, tree.CompareTo(actualTree));
        }

        [Theory]
        [InlineData(TestHelper.Platform)]
        public void JsonNET_FullTypeNameSerialization(string platform)
        {
            Node tree = SampleTree.Init();

            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                Converters = new JsonConverter[] { new StringEnumConverter() },
                TypeNameHandling = TypeNameHandling.All,
            };
            string expectedJson = JsonConvert.SerializeObject(tree, settings);
            Node deserialized = JsonConvert.DeserializeObject<Node>(expectedJson, settings);
            string actualJson = JsonConvert.SerializeObject(deserialized, settings);

            Assert.Equal(expectedJson, actualJson);
        }

        [Theory]
        [InlineData(TestHelper.Platform)]
        public void JsonNET_PropertySerialization(string platform)
        {
            Node tree = SampleTree.Init();

            var serializeSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                Converters = new JsonConverter[] { new StringEnumConverter() },
            };
            var deserializeSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                Converters = new JsonConverter[] { new StringEnumConverter(), new PropertyJsonConverter() },
            };
            string expectedJson = JsonConvert.SerializeObject(tree, serializeSettings);
            Node deserialized = JsonConvert.DeserializeObject<Node>(expectedJson, deserializeSettings);
            string actualJson = JsonConvert.SerializeObject(deserialized, serializeSettings);

            Assert.Equal(expectedJson, actualJson);
        }

        [Theory]
        [InlineData(TestHelper.Platform)]
        public void JsonNET_ClassNameSerialization(string platform)
        {
            Node tree = SampleTree.Init();

            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                Converters = new JsonConverter[] { new ClassNameJsonConverter() },
            };
            string expectedJson = JsonConvert.SerializeObject(tree, settings);
            Node deserialized = JsonConvert.DeserializeObject<Node>(expectedJson, settings);
            string actualJson = JsonConvert.SerializeObject(deserialized, settings);

            Assert.Equal(expectedJson, actualJson);
        }

        [Theory]
        [InlineData(TestHelper.Platform)]
        public void JsonNET_AttributeSerialization(string platform)
        {
            Node tree = SampleTree.Init();

            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                Converters = new JsonConverter[] { new AttributeJsonConverter() },
            };
            string expectedJson = JsonConvert.SerializeObject(tree, settings);
            Node deserialized = JsonConvert.DeserializeObject<Node>(expectedJson, settings);
            string actualJson = JsonConvert.SerializeObject(deserialized, settings);

            Assert.Equal(expectedJson, actualJson);
        }

        [Theory]
        [InlineData(TestHelper.Platform)]
        public void Protobuf_Seialization(string platform)
        {
            Statement tree = SampleTree.Init();

            Node actualTree;
            var proto = ProtoBuf.Serializer.GetProto<Statement>();
            using (var memoryStream = new System.IO.MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(memoryStream, tree);
                memoryStream.Position = 0;
                actualTree = ProtoBuf.Serializer.Deserialize<Statement>(memoryStream);
            }

            Assert.Equal(0, tree.CompareTo(actualTree));
        }

        [Theory]
        [InlineData(TestHelper.Platform)]
        public void ServiceStackJson_Serialization(string platform)
        {
            Statement tree = SampleTree.Init();
            var expectedJson = ServiceStack.Text.JsonSerializer.SerializeToString<Statement>(tree);
            Statement deserialized = ServiceStack.Text.JsonSerializer.DeserializeFromString<Statement>(expectedJson);
            string actualJson = ServiceStack.Text.JsonSerializer.SerializeToString<Statement>(deserialized);

            Assert.Equal(expectedJson, actualJson);
        }

        [Theory]
        [InlineData(TestHelper.Platform)]
        public void ServiceStackType_Serialization(string platform)
        {
            Statement tree = SampleTree.Init();
            var expectedJson = ServiceStack.Text.TypeSerializer.SerializeToString<Statement>(tree);
            Statement deserialized = ServiceStack.Text.TypeSerializer.DeserializeFromString<Statement>(expectedJson);
            string actualJson = ServiceStack.Text.TypeSerializer.SerializeToString<Statement>(deserialized);

            Assert.Equal(expectedJson, actualJson);
        }
    }
}
