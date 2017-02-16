using NUnit.Framework;
using ServiceStack;

namespace TreeProcessing.NET.Tests
{
    [TestFixture]
    public class MapperTests
    {
        [TestCase(TestHelper.Platform)]
        public void StaticMapper_ModelToDtoAndBack(string platform)
        {
            Statement sampleTree = SampleTree.Init();
            NodeDto sampleTreeDto = MapperHelper.ModelToDto(sampleTree);
            Node mappedBack = MapperHelper.DtoToModel(sampleTreeDto);

            Assert.AreEqual(sampleTree, mappedBack);
        }

        [TestCase(TestHelper.Platform)]
        public void DynamicMapper_ModelToDtoAndBack(string platform)
        {
            Statement sampleTree = SampleTree.Init();
            NodeDto sampleTreeDto = MapperHelper.ModelToDtoDynamicViaReflection(sampleTree);
            Node mappedBack = MapperHelper.DtoToModelViaReflection(sampleTreeDto);

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
