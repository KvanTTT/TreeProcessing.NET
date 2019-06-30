using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MessagePack;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.MemberReferenceExpression)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    [MessagePackObject]
    public class MemberReferenceExpression : Expression
    {
        [IgnoreMember]
        public override NodeType NodeType => NodeType.MemberReferenceExpression;

        [DataMember, ProtoMember(1), Key(0)]
        public Expression Target { get; set; }

        [DataMember, ProtoMember(2), Key(1)]
        public Identifier Name { get; set; }

        public MemberReferenceExpression(Expression target, Identifier name)
        {
            Target = target;
            Name = name;
        }

        public MemberReferenceExpression()
        {
        }

        public override int CompareTo(Node other)
        {
            int result = base.CompareTo(other);
            if (result != 0)
            {
                return result;
            }

            MemberReferenceExpression expr = (MemberReferenceExpression)other;
            result = Target.CompareTo(expr.Target);
            if (result != 0)
            {
                return result;
            }

            result = Name.CompareTo(expr.Name);
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
                result.Add(Target);
                result.Add(Name);
                return result;
            }
        }

        public override IEnumerable<Node> GetAllDescendants()
        {
            yield return Target;

            foreach (var targetDescendant in Target.GetAllDescendants())
            {
                yield return targetDescendant;
            }

            yield return Name;
        }

        public override string ToString()
        {
            return $"{Target}.{Name}";
        }

        public override TResult Accept<TResult>(IVisitor<TResult> nodeVisitor)
        {
            return nodeVisitor.Visit(this);
        }
    }
}
