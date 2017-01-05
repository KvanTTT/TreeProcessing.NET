using ProtoBuf;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TreesProcessing.NET
{
    public abstract class StatementDto : NodeDto
    {
        public override NodeType NodeType => NodeType.Statement;

        public StatementDto()
        {
        }
    }
}
