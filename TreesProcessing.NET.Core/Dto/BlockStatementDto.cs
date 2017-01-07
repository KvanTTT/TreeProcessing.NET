using System.Collections.Generic;

namespace TreesProcessing.NET
{
    public class BlockStatementDto : StatementDto
    {
        public override NodeType NodeType => NodeType.BlockStatement;

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
