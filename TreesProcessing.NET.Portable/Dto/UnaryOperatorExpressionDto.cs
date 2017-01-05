using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.UnaryOperatorExpression)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class UnaryOperatorExpressionDto : ExpressionDto
    {
        public override NodeType NodeType => NodeType.UnaryOperatorExpression;

        [DataMember]
        [ProtoMember(1)]
        public string Operator { get; set; }

        [DataMember]
        [ProtoMember(2)]
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
