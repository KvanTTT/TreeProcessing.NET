using System.Text;

namespace TreeProcessing.NET
{
    public class NodeDotRenderer
    {
        private const int NodeNameTrimLength = 20;

        private StringBuilder _vertexesString;
        private StringBuilder _edgesString;
        private int _currentIndex;

        public string Render(Node node)
        {
            _edgesString = new StringBuilder();
            _vertexesString = new StringBuilder();
            _currentIndex = 0;

            RenderNode(node);

            var graphString = new StringBuilder();
            graphString.AppendLine("digraph cfg {");
            graphString.Append(_vertexesString.ToString());
            graphString.Append(_edgesString.ToString());
            graphString.AppendLine("}");

            return graphString.ToString();
        }

        private void RenderNode(Node node)
        {
            int index = _currentIndex;
            _vertexesString.Append(index + " [label=\"" + FormatName(node.ToString()) + "\"");

            if (node is Statement)
            {
                _vertexesString.Append(", fillcolor=\"#99D2F2\", style=filled");
            }
            else if (node is Expression)
            {
                if (node is Token)
                {
                    _vertexesString.Append(", fillcolor=\"#A3D977\", style=filled");
                }
                else
                {
                    _vertexesString.Append(", fillcolor=\"#FCC438\", style=filled");
                }
            }
            _vertexesString.AppendLine("];");

            foreach (var child in node.Children)
            {
                if (child != null)
                {
                    _currentIndex++;
                    _edgesString.AppendLine(RenderEdge(index, _currentIndex));
                    RenderNode(child);
                }
            }
        }

        private string FormatName(string s)
        {
            if (s.Length > NodeNameTrimLength)
            {
                s = s.Remove(NodeNameTrimLength);
            }
            s = s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "").Replace("\n", "");
            return s;
        }

        private string RenderEdge(int startInd, int endInd, string advanced = "")
        {
            return $"{startInd}->{endInd}" + (advanced == "" ? "" : " " + advanced) + ";";
        }
    }
}
