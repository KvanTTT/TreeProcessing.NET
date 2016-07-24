using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NUnit.Framework;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TreesProcessing.NET.Tests
{
    [TestFixture]
    public class SerializationTests
    {
        [Test]
        public void Binary_Serialization()
        {
            Statement tree = SampleTree.Init();

            Statement actualTree;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, tree);

                memoryStream.Position = 0;
                var chars = new byte[memoryStream.Length];
                memoryStream.Read(chars, 0, (int)memoryStream.Length);
                var str = Encoding.Default.GetString(chars);

                memoryStream.Position = 0;
                actualTree = (Statement)binaryFormatter.Deserialize(memoryStream);
            }

            Assert.AreEqual(0, tree.CompareTo(actualTree));
        }

        [Test]
        public void Xml_Serialization()
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
                var str = Encoding.Default.GetString(chars);

                memoryStream.Position = 0;
                actualTree = (Statement)serializer.Deserialize(memoryStream);
            }

            Assert.AreEqual(0, tree.CompareTo(actualTree));
        }

        [Test]
        public void JsonNET_FullTypeNameSerialization()
        {
            Node tree = SampleTree.Init();

            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                Converters = new JsonConverter[] { new StringEnumConverter() },
                TypeNameHandling = TypeNameHandling.All,
            };
            string expectedJson = JsonConvert.SerializeObject(tree, settings);
            Node deserialzied = JsonConvert.DeserializeObject<Node>(expectedJson, settings);
            string actualJson = JsonConvert.SerializeObject(deserialzied, settings);

            Assert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void JsonNET_PropertySerialization()
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
            Node deserialzied = JsonConvert.DeserializeObject<Node>(expectedJson, deserializeSettings);
            string actualJson = JsonConvert.SerializeObject(deserialzied, serializeSettings);

            Assert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void JsonNET_ClassNameSerialization()
        {
            Node tree = SampleTree.Init();

            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                Converters = new JsonConverter[] { new ClassNameJsonConverter() },
            };
            string expectedJson = JsonConvert.SerializeObject(tree, settings);
            Node deserialzied = JsonConvert.DeserializeObject<Node>(expectedJson, settings);
            string actualJson = JsonConvert.SerializeObject(deserialzied, settings);

            Assert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void JsonNET_AttributeSerialization()
        {
            Node tree = SampleTree.Init();

            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                Converters = new JsonConverter[] { new AttributeJsonConverter() },
            };
            string expectedJson = JsonConvert.SerializeObject(tree, settings);
            Node deserialzied = JsonConvert.DeserializeObject<Node>(expectedJson, settings);
            string actualJson = JsonConvert.SerializeObject(deserialzied, settings);

            Assert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Protobuf_Seialization()
        {
            Statement tree = SampleTree.Init();

            var proto = ProtoBuf.Serializer.GetProto<Statement>();
            Node actualTree;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(memoryStream, tree);
                memoryStream.Position = 0;
                actualTree = ProtoBuf.Serializer.Deserialize<Statement>(memoryStream);
            }

            Assert.AreEqual(0, tree.CompareTo(actualTree));
        }
    }
}
