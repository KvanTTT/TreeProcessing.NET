using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.BooleanLiteral)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class BooleanLiteralDto : TerminalDto
    {
        public override NodeType NodeType => NodeType.BooleanLiteral;

        [DataMember]
        [ProtoMember(1)]
        public bool Value { get; set; }

        public BooleanLiteralDto(bool value)
        {
            Value = value;
        }

        public BooleanLiteralDto()
        {
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
