using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.UnaryOperatorExpression)]
#if PORTABLE || NET
    [Serializable]
#endif
    [DataContract]
    [ProtoContract]
    public class UnaryOperatorExpression : Expression
    {
        public override NodeType NodeType => NodeType.UnaryOperatorExpression;

        [DataMember]
        [ProtoMember(1)]
        public string Operator { get; set; }

        [DataMember]
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

        public override int GetHashCode()
        {
            return HashUtils.Combine(Operator.GetHashCode(), base.GetHashCode());
        }

        public override string ToString()
        {
            return $"{Operator}{Expression}";
        }
    }
}
