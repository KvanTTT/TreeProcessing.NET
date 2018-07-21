using System.Collections.Generic;

namespace TreeProcessing.NET
{
    public class ForStatementDto : StatementDto
    {
        public override NodeType NodeType => NodeType.ForStatement;

        public List<StatementDto> Initializers { get; set; }

        public ExpressionDto Condition { get; set; }

        public List<ExpressionDto> Iterators { get; set; }

        public StatementDto Statement { get; set; }

        public ForStatementDto(List<StatementDto> initializers, ExpressionDto condition, List<ExpressionDto> iterators, StatementDto statement)
        {
            Initializers = initializers;
            Condition = condition;
            Iterators = iterators;
            Statement = statement;
        }

        public ForStatementDto()
        {
            Initializers = new List<StatementDto>();
            Iterators = new List<ExpressionDto>();
        }

        public override string ToString()
        {
            return $"for ({(string.Join(" ", Initializers))} {Condition}; {(string.Join(" ", Iterators))}) {Statement}";
        }
    }
}
