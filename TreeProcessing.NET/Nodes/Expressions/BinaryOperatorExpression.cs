using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.BinaryOperatorExpression)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class BinaryOperatorExpression : Expression
    {
        public override NodeType NodeType => NodeType.BinaryOperatorExpression;

        [DataMember]
        [ProtoMember(1)]
        public Expression Left { get; set; }

        [DataMember]
        [ProtoMember(2)]
        public string Operator { get; set; }

        [DataMember]
        [ProtoMember(3)]
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

        public override IEnumerable<Node> AllDescendants
        {
            get
            {
                var result = new List<Node>();
                result.Add(Left);
                result.AddRange(Left.AllDescendants);
                result.Add(Right);
                result.AddRange(Right.AllDescendants);
                return result;
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
    }
}
