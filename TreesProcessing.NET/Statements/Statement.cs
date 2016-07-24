using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.Statement)]
    [ProtoContract]
    /*[ProtoInclude(50, typeof(BlockStatement))]
    [ProtoInclude(51, typeof(ExpressionStatement))]
    [ProtoInclude(52, typeof(ForStatement))]
    [ProtoInclude(53, typeof(IfElseStatement))]
    [ProtoInclude(54, typeof(NullLiteral))]*/
    public abstract class Statement : Node
    {
        public override NodeType NodeType => NodeType.Statement;

        public Statement()
        {
        }
    }
}
