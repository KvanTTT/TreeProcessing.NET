using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.MemberReferenceExpression)]
#if !CORE
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

        public override IEnumerable<Node> Descendants
        {
            get
            {
                var result = new List<Node>();
                result.Add(Target);
                result.AddRange(Target.Descendants);
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
