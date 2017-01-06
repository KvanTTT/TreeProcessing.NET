using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.ForStatement)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class ForStatementDto : StatementDto
    {
        public override NodeType NodeType => NodeType.ForStatement;

        [DataMember]
        [ProtoMember(1)]
        public List<StatementDto> Initializers { get; set; }

        [DataMember]
        [ProtoMember(2)]
        public ExpressionDto Condition { get; set; }

        [DataMember]
        [ProtoMember(3)]
        public List<ExpressionDto> Iterators { get; set; }

        [DataMember]
        [ProtoMember(4)]
        public StatementDto Statement { get; set; }

        public ForStatementDto(List<StatementDto> initializers, ExpressionDto condition, List<ExpressionDto> iterators, StatementDto statement)
        {
            Initializers = initializers;
            Condition = condition;
            Iterators = iterators;
            Statement = statement;
        }

        public ForStatementDto()
        {
            Initializers = new List<StatementDto>();
            Iterators = new List<ExpressionDto>();
        }

        public override string ToString()
        {
            return $"for ({(string.Join(" ", Initializers))} {Condition}; {(string.Join(" ", Iterators))}) {Statement}";
        }
    }
}
