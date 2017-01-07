namespace TreesProcessing.NET
{
    public class UnaryOperatorExpressionDto : ExpressionDto
    {
        public override NodeType NodeType => NodeType.UnaryOperatorExpression;

        public string Operator { get; set; }

        public ExpressionDto Expression { get; set; }

        public UnaryOperatorExpressionDto(string op, ExpressionDto expression)
        {
            Operator = op;
            Expression = expression;
        }

        public UnaryOperatorExpressionDto()
        {
        }

        public override string ToString()
        {
            return $"{Operator}{Expression}";
        }
    }
}
