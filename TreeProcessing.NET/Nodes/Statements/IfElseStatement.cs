using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.IfElseStatement)]
#if PORTABLE || NET
    [Serializable]
#endif
    [DataContract]
    [ProtoContract]
    public class IfElseStatement : Statement
    {
        public override NodeType NodeType => NodeType.IfElseStatement;

        [DataMember]
        [ProtoMember(1)]
        public Expression Condition { get; set; }

        [DataMember]
        [ProtoMember(2)]
        public Statement TrueStatement { get; set; }

        [DataMember]
        [ProtoMember(3)]
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

        public override IEnumerable<Node> AllDescendants
        {
            get
            {
                var result = new List<Node>();
                result.Add(Condition);
                result.AddRange(Condition.AllDescendants);
                result.Add(TrueStatement);
                result.AddRange(TrueStatement.AllDescendants);
                if (FalseStatement != null)
                {
                    result.Add(FalseStatement);
                    result.AddRange(FalseStatement.AllDescendants);
                }
                return result;
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
    }
}
