using System.Collections.Generic;

namespace TreesProcessing.NET
{
    public class InvocationExpressionDto : ExpressionDto
    {
        public override NodeType NodeType => NodeType.InvocationExpression;

        public ExpressionDto Target { get; set; }

        public List<ExpressionDto> Args { get; set; }

        public InvocationExpressionDto(ExpressionDto target, List<ExpressionDto> args)
        {
            Target = target;
            Args = args;
        }

        public InvocationExpressionDto()
        {
            Args = new List<ExpressionDto>();
        }

        public override string ToString()
        {
            return $"{Target}({(string.Join(", ", Args))})";
        }
    }
}
