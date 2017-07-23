using System.IO;
using Xunit;

namespace TreeProcessing.NET.Tests
{
    public class RenderTests
    {
        [Fact]
        public void Render_DotFromNodes_PngGraphFile()
        {
            var sampleTree = SampleTree.Init();

            var dotRenderer = new NodeDotRenderer();
            var dotString = dotRenderer.Render(sampleTree);

            var graphvizRenderer = new GraphvizRenderer();
            var filePath = Path.Combine(GraphvizRenderer.SolutionDirectory, @"TreeProcessing.NET.Tests\bin\SampleTree.png");
            graphvizRenderer.Render(filePath, dotString);
        }
    }
}
