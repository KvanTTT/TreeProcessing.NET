using ProtoBuf;
using System;
using System.Runtime.Serialization;
using MessagePack;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.FloatLiteral)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    [MessagePackObject]
    public class FloatLiteral : Token
    {
        [IgnoreMember]
        public override NodeType NodeType => NodeType.FloatLiteral;

        [DataMember, ProtoMember(1), Key(0)]
        public float Value { get; set; }

        public FloatLiteral(float value)
        {
            Value = value;
        }

        public FloatLiteral()
        {
        }

        public override int CompareTo(Node other)
        {
            int result = base.CompareTo(other);
            if (result != 0)
            {
                return result;
            }

            FloatLiteral expr = (FloatLiteral)other;
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
