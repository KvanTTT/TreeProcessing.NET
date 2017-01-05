using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.IfElseStatement)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class IfElseStatementDto : StatementDto
    {
        public override NodeType NodeType => NodeType.IfElseStatement;

        [DataMember]
        [ProtoMember(1)]
        public ExpressionDto Condition { get; set; }

        [DataMember]
        [ProtoMember(2)]
        public StatementDto TrueStatement { get; set; }

        [DataMember]
        [ProtoMember(3)]
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
