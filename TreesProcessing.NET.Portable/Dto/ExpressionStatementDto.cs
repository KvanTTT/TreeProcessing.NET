using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.ExpressionStatement)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class ExpressionStatementDto : StatementDto
    {
        public override NodeType NodeType => NodeType.ExpressionStatement;

        [DataMember]
        [ProtoMember(1)]
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
