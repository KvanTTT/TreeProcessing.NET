using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MessagePack;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.IfElseStatement)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    [MessagePackObject]
    public class IfElseStatement : Statement
    {
        [IgnoreMember]
        public override NodeType NodeType => NodeType.IfElseStatement;

        [DataMember, ProtoMember(1), Key(0)]
        public Expression Condition { get; set; }

        [DataMember, ProtoMember(2), Key(1)]
        public Statement TrueStatement { get; set; }

        [DataMember, ProtoMember(3), Key(2)]
        public Statement FalseStatement { get; set; }

        public IfElseStatement(Expression condition, Statement trueStatement, Statement falseStatement = null)
        {
            Condition = condition;
            TrueStatement = trueStatement;
            FalseStatement = falseStatement;
        }

        public IfElseStatement()
        {
        }

        public override int CompareTo(Node other)
        {
            int result = base.CompareTo(other);
            if (result != 0)
            {
                return result;
            }

            IfElseStatement statement = (IfElseStatement)other;
            result = Condition.CompareTo(statement.Condition);
            if (result != 0)
            {
                return result;
            }

            result = TrueStatement.CompareTo(statement.TrueStatement);
            if (result != 0)
            {
                return result;
            }

            if (FalseStatement == null && statement.FalseStatement != null)
            {
                return -(int)statement.FalseStatement.NodeType;
            }

            if (FalseStatement != null && statement.FalseStatement == null)
            {
                return (int)FalseStatement.NodeType;
            }

            if (FalseStatement != null && statement.FalseStatement != null)
            {
                return FalseStatement.CompareTo(statement.FalseStatement);
            }

            return 0;
        }

        [IgnoreMember]
        public override IEnumerable<Node> Children
        {
            get
            {
                var result = new List<Node>();
                result.Add(Condition);
                result.Add(TrueStatement);
                if (FalseStatement != null)
                {
                    result.Add(FalseStatement);
                }
                return result;
            }
        }

        public override IEnumerable<Node> GetAllDescendants()
        {
            yield return Condition;

            foreach (var descendant in Condition.GetAllDescendants())
            {
                yield return descendant;
            }

            yield return TrueStatement;

            foreach (var descendant in TrueStatement.GetAllDescendants())
            {
                yield return descendant;
            }

            if (FalseStatement != null)
            {
                yield return FalseStatement;

                foreach (var descendant in FalseStatement.GetAllDescendants())
                {
                    yield return descendant;
                }
            }
        }

        public override string ToString()
        {
            string result = $"if ({Condition}) {{{TrueStatement}}}";
            if (FalseStatement != null)
            {
                result += $" else {{{FalseStatement}}}";
            }
            return result;
        }

        public override TResult Accept<TResult>(IVisitor<TResult> nodeVisitor)
        {
            return nodeVisitor.Visit(this);
        }
    }
}
