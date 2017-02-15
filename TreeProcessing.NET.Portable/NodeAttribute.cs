using System;

namespace TreeProcessing.NET
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NodeAttr : Attribute
    {
        public NodeType NodeType { get; set; }

        public NodeAttr(NodeType nodeType)
        {
            NodeType = nodeType;
        }
    }
}
