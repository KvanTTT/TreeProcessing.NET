using Newtonsoft.Json;
using ProtoBuf;
using System.Collections.Generic;
using System.Linq;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.InvocationExpression)]
    [ProtoContract]
    public class InvocationExpression : Expression
    {
        public override NodeType NodeType => NodeType.InvocationExpression;

        public Expression Target { get; set; }

        [JsonIgnore]
        [ProtoMember(1, Name = nameof(Target))]
        public Node TargetSerializable
        {
            get { return Target; }
            set { Target = (Expression)value; }
        }

        public IList<Expression> Args { get; set; }

        [JsonIgnore]
        [ProtoMember(2, Name = nameof(Args), OverwriteList = true)]
        public IList<Node> ArgsSerializable
        {
            get { return Args.Select(s => (Node)s).ToList(); }
            set { Args = value.Select(s => (Expression)s).ToList(); }
        }

        public InvocationExpression(Expression target, IList<Expression> args)
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

        public override IEnumerable<Node> Descendants
        {
            get
            {
                var result = new List<Node>();
                result.Add(Target);
                result.AddRange(Target.Descendants);
                foreach (var arg in Args)
                {
                    result.Add(arg);
                    result.AddRange(arg.Descendants);
                }
                return result;
            }
        }

        public override string ToString()
        {
            return $"{Target}({(string.Join(", ", Args))})";
        }
    }
}
