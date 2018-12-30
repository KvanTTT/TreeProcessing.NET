using System;
using ProtoBuf;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MessagePack;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.InvocationExpression)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    [MessagePackObject]
    public class InvocationExpression : Expression
    {
        [IgnoreMember]
        public override NodeType NodeType => NodeType.InvocationExpression;

        [DataMember]
        [ProtoMember(1)]
        [Key(0)]
        public Expression Target { get; set; }

        [DataMember]
        [ProtoMember(2)]
        [Key(1)]
        public List<Expression> Args { get; set; }

        public InvocationExpression(Expression target, List<Expression> args)
        {
            Target = target;
            Args = args;
        }

        public InvocationExpression()
        {
            Args = new List<Expression>();
        }

        public override int CompareTo(Node other)
        {
            int result = base.CompareTo(other);
            if (result != 0)
            {
                return result;
            }

            InvocationExpression expr = (InvocationExpression)other;
            result = Target.CompareTo(expr.Target);
            if (result != 0)
            {
                return result;
            }

            if (Args.Count != expr.Args.Count)
            {
                return Args.Count - expr.Args.Count;
            }

            for (int i = 0; i < Args.Count; i++)
            {
                result = Args[i].CompareTo(expr.Args[i]);
                if (result != 0)
                {
                    return result;
                }
            }

            return 0;
        }

        [IgnoreMember]
        public override IEnumerable<Node> Children
        {
            get
            {
                var result = new List<Node>();
                result.Add(Target);
                foreach (var arg in Args)
                {
                    result.Add(arg);
                }
                return result;
            }
        }

        [IgnoreMember]
        public override IEnumerable<Node> AllDescendants
        {
            get
            {
                var result = new List<Node>();
                result.Add(Target);
                result.AddRange(Target.AllDescendants);
                foreach (var arg in Args)
                {
                    result.Add(arg);
                    result.AddRange(arg.AllDescendants);
                }
                return result;
            }
        }

        public override string ToString()
        {
            return $"{Target}({(string.Join(", ", Args))})";
        }

        public override TResult Accept<TResult>(IVisitor<TResult> nodeVisitor)
        {
            return nodeVisitor.Visit(this);
        }
    }
}
