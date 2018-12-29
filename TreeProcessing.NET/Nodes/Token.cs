using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MessagePack;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.Token)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    public abstract class Token: Expression
    {
        [IgnoreMember]
        public override NodeType NodeType => NodeType.Token;

        [IgnoreMember]
        public override IEnumerable<Node> Children => Enumerable.Empty<Node>();

        [IgnoreMember]
        public override IEnumerable<Node> AllDescendants => Enumerable.Empty<Node>();
    }
}
