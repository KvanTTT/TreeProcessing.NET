using NUnit.Framework;
namespace TreeProcessing.NET.Tests
{
    [TestFixture]
    public class MapperTests
    {
        [TestCase(TestHelper.Platform)]
        public void HandwrittenMapper_ModelToDtoAndBack(string platform)
        {
            Statement sampleTree = SampleTree.Init();
            NodeDto sampleTreeDto = MapperHelper.ModelToDto(sampleTree);
            Node mappedBack = MapperHelper.DtoToModel(sampleTreeDto);

            Assert.AreEqual(sampleTree, mappedBack);
        }

#if NET
        [TestCase(TestHelper.Platform)]
        public void AutoMapper_ModelToDtoAndBack(string platform)
        {
            AutoMapperHelper.Initialize();

            Statement sampleTree = SampleTree.Init();
            NodeDto sampleTreeDto = AutoMapper.Mapper.Map<NodeDto>(sampleTree);
            var mappedBack = AutoMapper.Mapper.Map<Node>(sampleTreeDto);

            Assert.AreEqual(sampleTree, mappedBack);
        }
#endif
    }
}
