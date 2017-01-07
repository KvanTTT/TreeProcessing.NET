using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.BinaryOperatorExpression)]
#if !CORE
    [Serializable]
#endif
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

        public override IEnumerable<Node> Descendants
        {
            get
            {
                var result = new List<Node>();
                result.Add(Left);
                result.AddRange(Left.Descendants);
                result.Add(Right);
                result.AddRange(Right.Descendants);
                return result;
            }
        }

        public override string ToString()
        {
            return $"{Left} {Operator} {Right}";
        }
    }
}
