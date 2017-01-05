using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.InvocationExpression)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class InvocationExpressionDto : ExpressionDto
    {
        public override NodeType NodeType => NodeType.InvocationExpression;

        [DataMember]
        [ProtoMember(1)]
        public ExpressionDto Target { get; set; }

        [DataMember]
        [ProtoMember(2)]
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
