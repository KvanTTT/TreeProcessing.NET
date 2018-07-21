namespace TreeProcessing.NET
{
    public abstract class ExpressionDto : NodeDto
    {
        public override NodeType NodeType => NodeType.Expression;

        public ExpressionDto()
        {
        }
    }
}
