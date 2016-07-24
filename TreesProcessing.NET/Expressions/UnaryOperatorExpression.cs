using ProtoBuf;
using System.Collections.Generic;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.UnaryOperatorExpression)]
    [ProtoContract]
    public class UnaryOperatorExpression : Expression
    {
        public override NodeType NodeType => NodeType.UnaryOperatorExpression;

        [ProtoMember(1)]
        public string Operator { get; set; }

        [ProtoMember(2)]
        public Expression Expression { get; set; }

        public UnaryOperatorExpression(string op, Expression expression)
        {
            Operator = op;
            Expression = expression;
        }

        public UnaryOperatorExpression()
        {
        }

        public override int CompareTo(Node other)
        {
            int result = base.CompareTo(other);
            if (result != 0)
            {
                return result;
            }

            UnaryOperatorExpression expr = (UnaryOperatorExpression)other;
            result = Operator.CompareTo(expr.Operator);
            if (result != 0)
            {
                return result;
            }

            result = Expression.CompareTo(expr.Expression);
            if (result != 0)
            {
                return result;
            }

            return 0;
        }

        public override IEnumerable<Node> Descendants
        {
            get
            {
                var result = new List<Node>();
                result.Add(Expression);
                result.AddRange(Expression.Descendants);
                return result;
            }
        }

        public override string ToString()
        {
            return $"{Operator}{Expression}";
        }
    }
}
