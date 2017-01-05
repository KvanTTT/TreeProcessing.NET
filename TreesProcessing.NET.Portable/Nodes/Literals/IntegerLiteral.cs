using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.IntegerLiteral)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class IntegerLiteral : Terminal
    {
        public override NodeType NodeType => NodeType.IntegerLiteral;

        [DataMember]
        [ProtoMember(1)]
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
