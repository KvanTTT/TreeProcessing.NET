using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.Terminal)]
    [ProtoContract]
    [Serializable]
    public abstract class TerminalDto : ExpressionDto
    {
        public override NodeType NodeType => NodeType.Terminal;
    }
}
