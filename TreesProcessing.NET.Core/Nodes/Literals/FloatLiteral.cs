using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.FloatLiteral)]
#if !CORE
    [Serializable]
#endif
    [DataContract]
    [ProtoContract]
    public class FloatLiteral : Terminal
    {
        public override NodeType NodeType => NodeType.FloatLiteral;

        [DataMember]
        [ProtoMember(1)]
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

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
