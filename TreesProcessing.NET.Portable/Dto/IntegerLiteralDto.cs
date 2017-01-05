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
    public class IntegerLiteralDto : TerminalDto
    {
        public override NodeType NodeType => NodeType.IntegerLiteral;

        [DataMember]
        [ProtoMember(1)]
        public int Value { get; set; }

        public IntegerLiteralDto(int value)
        {
            Value = value;
        }

        public IntegerLiteralDto()
        {
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
