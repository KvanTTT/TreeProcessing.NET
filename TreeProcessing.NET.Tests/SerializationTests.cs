using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using MessagePack;
using Xunit;

namespace TreeProcessing.NET.Tests
{
    public class SerializationTests
    {
        [Fact]
        public void Binary_Serialization()
        {
            Statement tree = SampleTree.Init();

            Statement actualTree;
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (var memoryStream = new MemoryStream())
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

        [Fact]
        public void DataContract_Serialization()
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

        [Fact]
        public void Xml_Serialization()
        {
            Statement tree = SampleTree.Init();
            XmlSerializer serializer = new XmlSerializer(typeof(Statement));

            Statement actualTree;
            using (var memoryStream = new MemoryStream())
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

        [Fact]
        public void JsonNET_FullTypeNameSerialization()
        {
            Node tree = SampleTree.Init();

            var settings = new JsonSerializerSettings
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

        [Fact]
        public void JsonNET_PropertySerialization()
        {
            Node tree = SampleTree.Init();

            var serializeSettings = new JsonSerializerSettings
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

        [Fact]
        public void JsonNET_ClassNameSerialization()
        {
            Node tree = SampleTree.Init();

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                Converters = new JsonConverter[] { new ClassNameJsonConverter() },
            };
            string expectedJson = JsonConvert.SerializeObject(tree, settings);
            Node deserialized = JsonConvert.DeserializeObject<Node>(expectedJson, settings);
            string actualJson = JsonConvert.SerializeObject(deserialized, settings);

            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void JsonNET_AttributeSerialization()
        {
            Node tree = SampleTree.Init();

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                Converters = new JsonConverter[] { new AttributeJsonConverter() },
            };
            string expectedJson = JsonConvert.SerializeObject(tree, settings);
            Node deserialized = JsonConvert.DeserializeObject<Node>(expectedJson, settings);
            string actualJson = JsonConvert.SerializeObject(deserialized, settings);

            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void Protobuf_Serialization()
        {
            Statement tree = SampleTree.Init();

            Node actualTree;
            using (var memoryStream = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(memoryStream, tree);
                memoryStream.Position = 0;
                actualTree = ProtoBuf.Serializer.Deserialize<Statement>(memoryStream);
            }

            using (var fileStream = new FileStream("test", FileMode.OpenOrCreate))
            {
                ProtoBuf.Serializer.Serialize(fileStream, tree);
            }

            Assert.Equal(0, tree.CompareTo(actualTree));
        }

        [Fact]
        public void ServiceStackJson_Serialization()
        {
            Statement tree = SampleTree.Init();
            var expectedJson = ServiceStack.Text.JsonSerializer.SerializeToString(tree);
            Statement deserialized = ServiceStack.Text.JsonSerializer.DeserializeFromString<Statement>(expectedJson);
            string actualJson = ServiceStack.Text.JsonSerializer.SerializeToString(deserialized);

            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void ServiceStackType_Serialization()
        {
            Statement tree = SampleTree.Init();
            var expectedJson = ServiceStack.Text.TypeSerializer.SerializeToString(tree);
            Statement deserialized = ServiceStack.Text.TypeSerializer.DeserializeFromString<Statement>(expectedJson);
            string actualJson = ServiceStack.Text.TypeSerializer.SerializeToString(deserialized);

            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public void MessagePack_Serialization()
        {
            Statement tree = SampleTree.Init();

            var bytes = MessagePackSerializer.Serialize(tree);
            var actualTree = MessagePackSerializer.Deserialize<Statement>(bytes);

            Assert.Equal(0, tree.CompareTo(actualTree));
        }
    }
}
