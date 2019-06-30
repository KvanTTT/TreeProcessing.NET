using System;

namespace TreeProcessing.NET
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NodeAttr : Attribute
    {
        public NodeType NodeType { get; }

        public NodeAttr(NodeType nodeType)
        {
            NodeType = nodeType;
        }
    }
}
