using ProtoBuf;
using System.Collections.Generic;
using System.Linq;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.Terminal)]
    [ProtoContract]
    public abstract class Terminal : Expression
    {
        public override NodeType NodeType => NodeType.Terminal;

        public override IEnumerable<Node> Descendants => Enumerable.Empty<Node>();
    }
}
