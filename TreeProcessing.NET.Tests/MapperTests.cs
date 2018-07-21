using Xunit;

namespace TreeProcessing.NET.Tests
{
    public class MapperTests
    {
        [Fact]
        public void StaticMapper_ModelToDtoAndBack()
        {
            Statement sampleTree = SampleTree.Init();
            NodeDto sampleTreeDto = MapperHelper.ModelToDto(sampleTree);
            Node mappedBack = MapperHelper.DtoToModel(sampleTreeDto);

            Assert.Equal(sampleTree, mappedBack);
        }

        [Fact]
        public void DynamicMapper_ModelToDtoAndBack()
        {
            Statement sampleTree = SampleTree.Init();
            NodeDto sampleTreeDto = MapperHelper.ModelToDtoDynamicViaReflection(sampleTree);
            Node mappedBack = MapperHelper.DtoToModelViaReflection(sampleTreeDto);

            Assert.Equal(sampleTree, mappedBack);
        }

        [Fact]
        public void AutoMapper_ModelToDtoAndBack()
        {
            AutoMapperHelper.Initialize();

            Statement sampleTree = SampleTree.Init();
            NodeDto sampleTreeDto = AutoMapper.Mapper.Map<NodeDto>(sampleTree);
            var mappedBack = AutoMapper.Mapper.Map<Node>(sampleTreeDto);

            Assert.Equal(sampleTree, mappedBack);
        }
    }
}
