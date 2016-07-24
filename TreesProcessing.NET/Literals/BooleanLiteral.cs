using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.BooleanLiteral)]
    [Serializable]
    [ProtoContract]
    public class BooleanLiteral : Terminal
    {
        public override NodeType NodeType => NodeType.BooleanLiteral;

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

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
