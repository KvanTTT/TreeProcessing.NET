using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MessagePack;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.Node)]
    [Serializable]
    [DataContract]

    [ProtoContract]

    [MessagePackObject]
    [Union((int)NodeType.BinaryOperatorExpression, typeof(BinaryOperatorExpression))]
    [Union((int)NodeType.InvocationExpression, typeof(InvocationExpression))]
    [Union((int)NodeType.MemberReferenceExpression, typeof(MemberReferenceExpression))]
    [Union((int)NodeType.UnaryOperatorExpression, typeof(UnaryOperatorExpression))]
    [Union((int)NodeType.BooleanLiteral, typeof(BooleanLiteral))]
    [Union((int)NodeType.FloatLiteral, typeof(FloatLiteral))]
    [Union((int)NodeType.Identifier, typeof(Identifier))]
    [Union((int)NodeType.IntegerLiteral, typeof(IntegerLiteral))]
    [Union((int)NodeType.NullLiteral, typeof(NullLiteral))]
    [Union((int)NodeType.StringLiteral, typeof(StringLiteral))]
    [Union((int)NodeType.BlockStatement, typeof(BlockStatement))]
    [Union((int)NodeType.ExpressionStatement, typeof(ExpressionStatement))]
    [Union((int)NodeType.ForStatement, typeof(ForStatement))]
    [Union((int)NodeType.IfElseStatement, typeof(IfElseStatement))]
    public abstract class Node : IComparable, IComparable<Node>, IEquatable<Node>
    {
        [JsonProperty]
        [IgnoreMember]
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

        [IgnoreMember]
        public abstract IEnumerable<Node> Children { get; }

        public abstract IEnumerable<Node> GetAllDescendants();

        public override int GetHashCode()
        {
            int result = 0;

            foreach (Node child in Children)
            {
                result = HashUtils.Combine(result, child.GetHashCode());
            }

            return result;
        }

        public abstract TResult Accept<TResult>(IVisitor<TResult> nodeVisitor);
    }
}
