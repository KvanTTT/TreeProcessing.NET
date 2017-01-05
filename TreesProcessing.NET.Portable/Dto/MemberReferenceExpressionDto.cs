using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.MemberReferenceExpression)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class MemberReferenceExpressionDto : ExpressionDto
    {
        public override NodeType NodeType => NodeType.MemberReferenceExpression;

        [DataMember]
        [ProtoMember(1)]
        public ExpressionDto Target { get; set; }

        [DataMember]
        [ProtoMember(2)]
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
