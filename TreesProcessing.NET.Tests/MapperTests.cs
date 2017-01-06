using AutoMapper;
using NUnit.Framework;

namespace TreesProcessing.NET.Tests
{
    [TestFixture]
    public class MapperTests
    {
        [Test]
        public void Map_ModelToDtoAndBack()
        {
            MapperHelper.Initialize();

            Statement sampleTree = SampleTree.Init();
            NodeDto sampleTreeDto = Mapper.Map<NodeDto>(sampleTree);
            var mappedBack = Mapper.Map<Node>(sampleTreeDto);

            Assert.AreEqual(sampleTree, mappedBack);
        }
    }
}
