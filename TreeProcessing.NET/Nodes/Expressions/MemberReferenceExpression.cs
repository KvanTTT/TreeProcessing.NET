using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.MemberReferenceExpression)]
#if PORTABLE || NET
    [Serializable]
#endif
    [DataContract]
    [ProtoContract]
    public class MemberReferenceExpression : Expression
    {
        public override NodeType NodeType => NodeType.MemberReferenceExpression;

        [DataMember]
        [ProtoMember(1)]
        public Expression Target { get; set; }

        [DataMember]
        [ProtoMember(2)]
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

        public override IEnumerable<Node> AllDescendants
        {
            get
            {
                var result = new List<Node>();
                result.Add(Target);
                result.AddRange(Target.AllDescendants);
                result.Add(Name);
                return result;
            }
        }

        public override string ToString()
        {
            return $"{Target}.{Name}";
        }
    }
}
