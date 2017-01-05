using System;

namespace TreesProcessing.NET
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
