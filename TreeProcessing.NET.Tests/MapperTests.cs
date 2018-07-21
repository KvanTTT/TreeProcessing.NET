using Xunit;

namespace TreeProcessing.NET.Tests
{
    public class MapperTests
    {
        [Theory]
        [InlineData(TestHelper.Platform)]
        public void StaticMapper_ModelToDtoAndBack(string platform)
        {
            Statement sampleTree = SampleTree.Init();
            NodeDto sampleTreeDto = MapperHelper.ModelToDto(sampleTree);
            Node mappedBack = MapperHelper.DtoToModel(sampleTreeDto);

            Assert.Equal(sampleTree, mappedBack);
        }

        [Theory]
        [InlineData(TestHelper.Platform)]
        public void DynamicMapper_ModelToDtoAndBack(string platform)
        {
            Statement sampleTree = SampleTree.Init();
            NodeDto sampleTreeDto = MapperHelper.ModelToDtoDynamicViaReflection(sampleTree);
            Node mappedBack = MapperHelper.DtoToModelViaReflection(sampleTreeDto);

            Assert.Equal(sampleTree, mappedBack);
        }

#if NET
        [Theory]
        [InlineData(TestHelper.Platform)]
        public void AutoMapper_ModelToDtoAndBack(string platform)
        {
            AutoMapperHelper.Initialize();

            Statement sampleTree = SampleTree.Init();
            NodeDto sampleTreeDto = AutoMapper.Mapper.Map<NodeDto>(sampleTree);
            var mappedBack = AutoMapper.Mapper.Map<Node>(sampleTreeDto);

            Assert.Equal(sampleTree, mappedBack);
        }
#endif
    }
}
