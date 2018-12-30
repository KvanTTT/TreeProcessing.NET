using ProtoBuf;
using System;
using System.Runtime.Serialization;
using MessagePack;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.NullLiteral)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    [MessagePackObject]
    public class NullLiteral : Token
    {
        [IgnoreMember]
        public override NodeType NodeType => NodeType.NullLiteral;

        public NullLiteral()
        {
        }

        public override int GetHashCode() => "null".GetHashCode();

        public override string ToString()
        {
            return "null";
        }

        public override TResult Accept<TResult>(IVisitor<TResult> nodeVisitor)
        {
            return nodeVisitor.Visit(this);
        }
    }
}
