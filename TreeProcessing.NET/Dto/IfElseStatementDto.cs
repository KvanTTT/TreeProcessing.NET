namespace TreeProcessing.NET
{
    public class IfElseStatementDto : StatementDto
    {
        public override NodeType NodeType => NodeType.IfElseStatement;

        public ExpressionDto Condition { get; set; }

        public StatementDto TrueStatement { get; set; }

        public StatementDto FalseStatement { get; set; }

        public IfElseStatementDto(ExpressionDto condition, StatementDto trueStatement, StatementDto falseStatement = null)
        {
            Condition = condition;
            TrueStatement = trueStatement;
            FalseStatement = falseStatement;
        }

        public IfElseStatementDto()
        {
        }

        public override string ToString()
        {
            string result = $"if ({Condition}) {{{TrueStatement}}}";
            if (FalseStatement != null)
            {
                result += $" else {{{FalseStatement}}}";
            }
            return result;
        }
    }
}
