using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.Terminal)]
    [ProtoContract]
    public abstract class Terminal : Expression
    {
        public override NodeType NodeType => NodeType.Terminal;

        public override IEnumerable<Node> Descendants
        {
            get
            {
                return Enumerable.Empty<Node>();
            }
        }
    }
}
