using ProtoBuf;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.Expression)]
    [ProtoContract]
    public abstract class Expression : Node
    {
        public override NodeType NodeType => NodeType.Expression;

        public Expression()
        {
        }
    }
}
