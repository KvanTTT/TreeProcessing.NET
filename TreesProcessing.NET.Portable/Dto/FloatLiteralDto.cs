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
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class FloatLiteralDto : TerminalDto
    {
        public override NodeType NodeType => NodeType.FloatLiteral;

        [DataMember]
        [ProtoMember(1)]
        public float Value { get; set; }

        public FloatLiteralDto(float value)
        {
            Value = value;
        }

        public FloatLiteralDto()
        {
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
