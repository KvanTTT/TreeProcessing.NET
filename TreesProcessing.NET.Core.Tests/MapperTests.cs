using NUnit.Framework;
namespace TreesProcessing.NET.Tests
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
    }
}
