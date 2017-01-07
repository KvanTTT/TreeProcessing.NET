using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET.Tests
{
    [TestFixture]
    public class MapperTests
    {
        [Test]
        public void HandwrittenMapper_ModelToDtoAndBack()
        {
            Statement sampleTree = SampleTree.Init();
            NodeDto sampleTreeDto = MapperHelper.ModelToDto(sampleTree);
            Node mappedBack = MapperHelper.DtoToModel(sampleTreeDto);

            Assert.AreEqual(sampleTree, mappedBack);
        }
    }
}
