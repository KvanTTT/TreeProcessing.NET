using AutoMapper;
using NUnit.Framework;

namespace TreesProcessing.NET.Tests
{
    [TestFixture]
    public class MapperTests
    {
        [TestCase(TestHelper.Platform)]
        public void AutoMapper_ModelToDtoAndBack(string platform)
        {
            AutoMapperHelper.Initialize();

            Statement sampleTree = SampleTree.Init();
            NodeDto sampleTreeDto = Mapper.Map<NodeDto>(sampleTree);
            var mappedBack = Mapper.Map<Node>(sampleTreeDto);

            Assert.AreEqual(sampleTree, mappedBack);
        }
    }
}
