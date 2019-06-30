using ProtoBuf;
using System;
using System.Runtime.Serialization;
using MessagePack;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.StringLiteral)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    [MessagePackObject]
    public class StringLiteral : Token
    {
        [IgnoreMember]
        public override NodeType NodeType => NodeType.StringLiteral;

        [DataMember, ProtoMember(1), Key(0)]
        public string Value { get; set; }

        public StringLiteral(string value)
        {
            Value = value;
        }

        public StringLiteral()
        {
        }

        public override int CompareTo(Node other)
        {
            int result = base.CompareTo(other);
            if (result != 0)
            {
                return result;
            }

            StringLiteral expr = (StringLiteral)other;
            result = Value.CompareTo(expr.Value);
            if (result != 0)
            {
                return result;
            }

            return 0;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString()
        {
            return Value;
        }

        public override TResult Accept<TResult>(IVisitor<TResult> nodeVisitor)
        {
            return nodeVisitor.Visit(this);
        }
    }
}
