using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.NullLiteral)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class NullLiteralDto : TerminalDto
    {
        public override NodeType NodeType => NodeType.NullLiteral;

        public NullLiteralDto()
        {
        }

        public override string ToString()
        {
            return "null";
        }
    }
}
