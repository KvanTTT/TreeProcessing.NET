using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.Terminal)]
    [ProtoContract]
#if PORTABLE || NET
    [Serializable]
#endif
    public abstract class Terminal : Expression
    {
        public override NodeType NodeType => NodeType.Terminal;

        public override IEnumerable<Node> Children => Enumerable.Empty<Node>();

        public override IEnumerable<Node> AllDescendants => Enumerable.Empty<Node>();
    }
}
