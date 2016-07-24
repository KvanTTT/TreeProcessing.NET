using Newtonsoft.Json;
using ProtoBuf;
using System.Collections.Generic;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.IfElseStatement)]
    [ProtoContract]
    public class IfElseStatement : Statement
    {
        public override NodeType NodeType => NodeType.IfElseStatement;
        
        public Expression Condition { get; set; }

        public Statement TrueStatement { get; set; }

        public Statement FalseStatement { get; set; }

        [JsonIgnore]
        [ProtoMember(1, Name = nameof(Condition))]
        public Node ConditionSerializable { get { return Condition; } set { Condition = (Expression)value; } }

        [JsonIgnore]
        [ProtoMember(2, Name = nameof(TrueStatement))]
        public Node TrueStatementSerializable { get { return TrueStatement; } set { TrueStatement = (Statement)value; } }

        [JsonIgnore]
        [ProtoMember(3, Name = nameof(FalseStatement))]
        public Node FalseStatementSerializable { get { return FalseStatement; } set { FalseStatement = (Statement)value; } }

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

        public override IEnumerable<Node> Descendants
        {
            get
            {
                var result = new List<Node>();
                result.Add(Condition);
                result.AddRange(Condition.Descendants);
                result.Add(TrueStatement);
                result.AddRange(TrueStatement.Descendants);
                if (FalseStatement != null)
                {
                    result.Add(FalseStatement);
                    result.AddRange(FalseStatement.Descendants);
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
