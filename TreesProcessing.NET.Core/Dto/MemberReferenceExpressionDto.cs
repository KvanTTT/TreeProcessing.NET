namespace TreesProcessing.NET
{
    public class MemberReferenceExpressionDto : ExpressionDto
    {
        public override NodeType NodeType => NodeType.MemberReferenceExpression;

        public ExpressionDto Target { get; set; }

        public IdentifierDto Name { get; set; }

        public MemberReferenceExpressionDto(ExpressionDto target, IdentifierDto name)
        {
            Target = target;
            Name = name;
        }

        public MemberReferenceExpressionDto()
        {
        }

        public override string ToString()
        {
            return $"{Target}.{Name}";
        }
    }
}
