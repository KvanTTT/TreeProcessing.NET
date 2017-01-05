using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.StringLiteral)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class StringLiteralDto : TerminalDto
    {
        public override NodeType NodeType => NodeType.StringLiteral;

        [DataMember]
        [ProtoMember(1)]
        public string Value { get; set; }

        public StringLiteralDto(string value)
        {
            Value = value;
        }

        public StringLiteralDto()
        {
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
