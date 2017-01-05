using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.Identifier)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class IdentifierDto : TerminalDto
    {
        public override NodeType NodeType => NodeType.Identifier;

        [DataMember]
        [ProtoMember(1)]
        public string Id { get; set; }

        public IdentifierDto(string id)
        {
            Id = id;
        }

        public IdentifierDto()
        {
        }

        public override string ToString()
        {
            return Id;
        }
    }
}
