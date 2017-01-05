using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.BlockStatement)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class BlockStatementDto : StatementDto
    {
        public override NodeType NodeType => NodeType.BlockStatement;

        [DataMember]
        [ProtoMember(1)]
        public List<StatementDto> Statements { get; set; }

        public BlockStatementDto(List<StatementDto> statements)
        {
            Statements = statements;
        }

        public BlockStatementDto()
        {
        }

        public override string ToString()
        {
            return string.Join(" ", Statements);
        }
    }
}
