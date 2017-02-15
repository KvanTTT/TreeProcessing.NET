using ProtoBuf;
using System;
using System.Runtime.Serialization;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.BooleanLiteral)]
#if PORTABLE || NET
    [Serializable]
#endif
    [DataContract]
    [ProtoContract]
    public class BooleanLiteral : Terminal
    {
        public override NodeType NodeType => NodeType.BooleanLiteral;

        [DataMember]
        [ProtoMember(1)]
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
    }
}
