using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MessagePack;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.BlockStatement)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    [MessagePackObject]
    public class BlockStatement : Statement
    {
        [IgnoreMember]
        public override NodeType NodeType => NodeType.BlockStatement;

        [DataMember]
        [ProtoMember(1)]
        [Key(0)]
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

        [IgnoreMember]
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

        [IgnoreMember]
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

        public override TResult Accept<TResult>(IVisitor<TResult> nodeVisitor)
        {
            return nodeVisitor.Visit(this);
        }
    }
}
