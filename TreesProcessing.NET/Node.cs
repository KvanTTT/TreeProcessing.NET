using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.Node)]
    [ProtoContract]

    [ProtoInclude(10, typeof(BinaryOperatorExpression))]
    [ProtoInclude(11, typeof(InvocationExpression))]
    [ProtoInclude(12, typeof(MemberReferenceExpression))]
    [ProtoInclude(13, typeof(UnaryOperatorExpression))]

    [ProtoInclude(20, typeof(BooleanLiteral))]
    [ProtoInclude(21, typeof(FloatLiteral))]
    [ProtoInclude(22, typeof(Identifier))]
    [ProtoInclude(23, typeof(IntegerLiteral))]
    [ProtoInclude(24, typeof(NullLiteral))]
    [ProtoInclude(25, typeof(StringLiteral))]

    [ProtoInclude(30, typeof(BlockStatement))]
    [ProtoInclude(31, typeof(ExpressionStatement))]
    [ProtoInclude(32, typeof(ForStatement))]
    [ProtoInclude(33, typeof(IfElseStatement))]
    public abstract class Node : IComparable, IComparable<Node>, IEquatable<Node>
    {
        public abstract NodeType NodeType { get; }

        public bool Equals(Node other)
        {
            return CompareTo(other) == 0;
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as Node);
        }

        public virtual int CompareTo(Node other)
        {
            if (other == null)
            {
                return (int)NodeType;
            }

            if (NodeType != other.NodeType)
            {
                return NodeType - other.NodeType;
            }

            return 0;
        }

        public abstract IEnumerable<Node> Descendants { get; }
    }
}
