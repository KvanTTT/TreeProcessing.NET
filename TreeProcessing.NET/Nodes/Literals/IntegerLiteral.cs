using ProtoBuf;
using System;
using System.Runtime.Serialization;
using MessagePack;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.IntegerLiteral)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    [MessagePackObject]
    public class IntegerLiteral : Token
    {
        [IgnoreMember]
        public override NodeType NodeType => NodeType.IntegerLiteral;

        [DataMember]
        [ProtoMember(1)]
        [Key(0)]
        public int Value { get; set; }

        public IntegerLiteral(int value)
        {
            Value = value;
        }

        public IntegerLiteral()
        {
        }

        public override int CompareTo(Node other)
        {
            int result = base.CompareTo(other);
            if (result != 0)
            {
                return result;
            }

            IntegerLiteral expr = (IntegerLiteral)other;
            result = Value.CompareTo(expr.Value);
            if (result != 0)
            {
                return result;
            }

            return 0;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
