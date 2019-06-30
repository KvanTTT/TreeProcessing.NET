using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MessagePack;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.BinaryOperatorExpression)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    [MessagePackObject]
    public class BinaryOperatorExpression : Expression
    {
        [IgnoreMember]
        public override NodeType NodeType => NodeType.BinaryOperatorExpression;

        [DataMember]
        [ProtoMember(1)]
        [Key(0)]
        public Expression Left { get; set; }

        [DataMember]
        [ProtoMember(2)]
        [Key(1)]
        public string Operator { get; set; }

        [DataMember]
        [ProtoMember(3)]
        [Key(2)]
        public Expression Right { get; set; }

        public BinaryOperatorExpression(Expression left, string op, Expression right)
        {
            Left = left;
            Operator = op;
            Right = right;
        }

        public BinaryOperatorExpression()
        {
        }

        public override int CompareTo(Node other)
        {
            int result = base.CompareTo(other);
            if (result != 0)
            {
                return result;
            }

            BinaryOperatorExpression expr = (BinaryOperatorExpression)other;
            result = Left.CompareTo(expr.Left);
            if (result != 0)
            {
                return result;
            }

            result = Operator.CompareTo(expr.Operator);
            if (result != 0)
            {
                return result;
            }

            result = Right.CompareTo(expr.Right);
            if (result != 0)
            {
                return result;
            }

            return 0;
        }

        [IgnoreMember]
        public override IEnumerable<Node> Children
        {
            get
            {
                var result = new List<Node>();
                result.Add(Left);
                result.Add(Right);
                return result;
            }
        }

        public override IEnumerable<Node> GetAllDescendants()
        {
            yield return Left;

            foreach (var descendant in Left.GetAllDescendants())
            {
                yield return descendant;
            }

            yield return Right;

            foreach (var descendant in Right.GetAllDescendants())
            {
                yield return descendant;
            }
        }

        public override string ToString()
        {
            return $"{Left} {Operator} {Right}";
        }

        public override int GetHashCode()
        {
            return HashUtils.Combine(base.GetHashCode(), Operator.GetHashCode());
        }

        public override TResult Accept<TResult>(IVisitor<TResult> nodeVisitor)
        {
            Left.Accept(nodeVisitor);
            Right.Accept(nodeVisitor);

            return default;
        }
    }
}
