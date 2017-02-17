using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.Token)]
    [ProtoContract]
#if PORTABLE || NET
    [Serializable]
#endif
    public abstract class Token: Expression
    {
        public override NodeType NodeType => NodeType.Token;

        public override IEnumerable<Node> Children => Enumerable.Empty<Node>();

        public override IEnumerable<Node> AllDescendants => Enumerable.Empty<Node>();
    }
}
