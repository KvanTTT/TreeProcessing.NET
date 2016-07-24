using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.StringLiteral)]
    [Serializable]
    [ProtoContract]
    public class StringLiteral : Terminal
    {
        public override NodeType NodeType => NodeType.StringLiteral;

        [ProtoMember(1)]
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

        public override string ToString()
        {
            return Value;
        }
    }
}
