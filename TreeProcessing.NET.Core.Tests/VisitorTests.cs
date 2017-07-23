using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TreeProcessing.NET.Tests
{
    public class VisitorTests
    {
#if PORTABLE || NET
        [Theory]
        [InlineData(TestHelper.Platform)]
        public void CheckAllVisitorMethodsExists(string platform)
        {
            MethodInfo[] visitorMethods = typeof(IVisitor<>).GetMethods();
            IEnumerable<Type> nodeTypes = Assembly.GetAssembly(typeof(Node)).GetTypes()
                .Where(t => t == typeof(Node) || t.IsSubclassOf(typeof(Node)));
            foreach (Type type in nodeTypes)
            {
                Assert.True(visitorMethods
                    .FirstOrDefault(methodInfo =>
                    {
                        var parameters = methodInfo.GetParameters();
                        return parameters.Length > 0 && parameters[0].ParameterType == type;
                    }) != null,
                    $"Visitor for Type {type} is not exists");
            }
        }
#endif

        [Theory]
        [InlineData(TestHelper.Platform)]
        public void Visitor_Static(string platform)
        {
            TestVisitor(new StaticVisitor());
        }

        [Theory]
        [InlineData(TestHelper.Platform)]
        public void Visitor_Dynamic(string platform)
        {
            TestVisitor(new DynamicVisitor());
        }

        private static void TestVisitor(IVisitor<Node> visitor)
        {
            var serializeSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                Converters = new JsonConverter[] { new StringEnumConverter() },
            };

            Node tree = SampleTree.Init();
            string expectedJson = JsonConvert.SerializeObject(tree, serializeSettings);
            
            var processedTree = visitor.Visit(tree);
            string actualJson = JsonConvert.SerializeObject(processedTree, serializeSettings);

            Assert.Equal(expectedJson, actualJson);
        }
    }
}
