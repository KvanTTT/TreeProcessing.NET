namespace TreesProcessing.NET
{
    public class ExpressionStatementDto : StatementDto
    {
        public override NodeType NodeType => NodeType.ExpressionStatement;

        public ExpressionDto Expression { get; set; }

        public ExpressionStatementDto(ExpressionDto expression)
        {
            Expression = expression;
        }

        public ExpressionStatementDto()
        {
        }

        public override string ToString()
        {
            return Expression + ";";
        }
    }
}
