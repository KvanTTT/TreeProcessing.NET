using ProtoBuf;
using System;
using System.Runtime.Serialization;
using MessagePack;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.BooleanLiteral)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    [MessagePackObject]
    public class BooleanLiteral : Token
    {
        [IgnoreMember]
        public override NodeType NodeType => NodeType.BooleanLiteral;

        [DataMember]
        [ProtoMember(1)]
        [Key(0)]
        public bool Value { get; set; }

        public BooleanLiteral(bool value)
        {
            Value = value;
        }

        public BooleanLiteral()
        {
        }

        public override int CompareTo(Node other)
        {
            int result = base.CompareTo(other);
            if (result != 0)
            {
                return result;
            }

            BooleanLiteral expr = (BooleanLiteral)other;
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
            return Value.ToString();
        }

        public override TResult Accept<TResult>(IVisitor<TResult> nodeVisitor)
        {
            return nodeVisitor.Visit(this);
        }
    }
}
