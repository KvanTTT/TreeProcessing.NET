using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.BlockStatement)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class BlockStatement : Statement
    {
        public override NodeType NodeType => NodeType.BlockStatement;

        [DataMember]
        [ProtoMember(1)]
        public List<Statement> Statements { get; set; }

        public BlockStatement(List<Statement> statements)
        {
            Statements = statements;
        }

        public BlockStatement()
        {
        }

        public override int CompareTo(Node other)
        {
            int result = base.CompareTo(other);
            if (result != 0)
            {
                return result;
            }

            BlockStatement statement = (BlockStatement)other;
            if (Statements.Count != statement.Statements.Count)
            {
                return Statements.Count - statement.Statements.Count;
            }

            for (int i = 0; i < Statements.Count; i++)
            {
                result = Statements[i].CompareTo(statement.Statements[i]);
                if (result != 0)
                {
                    return result;
                }
            }

            return 0;
        }

        public override IEnumerable<Node> Children
        {
            get
            {
                var result = new List<Node>();
                foreach (var statement in Statements)
                {
                    result.Add(statement);
                }
                return result;
            }
        }

        public override IEnumerable<Node> AllDescendants
        {
            get
            {
                var result = new List<Node>();
                foreach (var statement in Statements)
                {
                    result.Add(statement);
                    result.AddRange(statement.AllDescendants);
                }
                return result;
            }
        }

        public override string ToString()
        {
            return string.Join(" ", Statements);
        }
    }
}
