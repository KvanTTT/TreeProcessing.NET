using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
