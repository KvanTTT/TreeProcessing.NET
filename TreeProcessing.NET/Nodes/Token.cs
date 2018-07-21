using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.Token)]
#if PORTABLE || NET
    [Serializable]
#endif
    [DataContract]
    [ProtoContract]
    public abstract class Token: Expression
    {
        public override NodeType NodeType => NodeType.Token;

        public override IEnumerable<Node> Children => Enumerable.Empty<Node>();

        public override IEnumerable<Node> AllDescendants => Enumerable.Empty<Node>();
    }
}
