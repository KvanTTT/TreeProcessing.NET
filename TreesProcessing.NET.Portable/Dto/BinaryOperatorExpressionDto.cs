using ProtoBuf;
using System;
using System.Runtime.Serialization;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.BinaryOperatorExpression)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class BinaryOperatorExpressionDto : ExpressionDto
    {
        public override NodeType NodeType => NodeType.BinaryOperatorExpression;

        public ExpressionDto Left { get; set; }

        public string Operator { get; set; }

        public ExpressionDto Right { get; set; }

        public BinaryOperatorExpressionDto(ExpressionDto left, string op, ExpressionDto right)
        {
            Left = left;
            Operator = op;
            Right = right;
        }

        public BinaryOperatorExpressionDto()
        {
        }

        public override string ToString()
        {
            return $"{Left} {Operator} {Right}";
        }
    }
}
