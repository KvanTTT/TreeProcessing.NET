using ProtoBuf;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TreesProcessing.NET
{
    public abstract class ExpressionDto : NodeDto
    {
        public override NodeType NodeType => NodeType.Expression;

        public ExpressionDto()
        {
        }
    }
}
