using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MessagePack;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.ForStatement)]
    [Serializable]
    [DataContract]
    [ProtoContract]

    [MessagePackObject]
    public class ForStatement : Statement
    {
        [IgnoreMember]
        public override NodeType NodeType => NodeType.ForStatement;

        [DataMember]
        [ProtoMember(1)]
        [Key(0)]
        public List<Statement> Initializers { get; set; }

        [DataMember]
        [ProtoMember(2)]
        [Key(1)]
        public Expression Condition { get; set; }

        [DataMember]
        [ProtoMember(3)]
        [Key(2)]
        public List<Expression> Iterators { get; set; }

        [DataMember]
        [ProtoMember(4)]
        [Key(3)]
        public Statement Statement { get; set; }

        public ForStatement(List<Statement> initializers, Expression condition, List<Expression> iterators, Statement statement)
        {
            Initializers = initializers;
            Condition = condition;
            Iterators = iterators;
            Statement = statement;
        }

        public ForStatement()
        {
            Initializers = new List<Statement>();
            Iterators = new List<Expression>();
        }

        public override int CompareTo(Node other)
        {
            int result = base.CompareTo(other);
            if (result != 0)
            {
                return result;
            }

            ForStatement statement = (ForStatement)other;
            if (Initializers.Count != statement.Initializers.Count)
            {
                return Initializers.Count - statement.Initializers.Count;
            }
            for (int i = 0; i < Initializers.Count; i++)
            {
                result = Initializers[i].CompareTo(statement.Initializers[i]);
                if (result != 0)
                {
                    return result;
                }
            }

            result = Condition.CompareTo(statement.Condition);
            if (result != 0)
            {
                return result;
            }

            if (Iterators.Count != statement.Iterators.Count)
            {
                return Iterators.Count - statement.Iterators.Count;
            }
            for (int i = 0; i < Iterators.Count; i++)
            {
                result = Iterators[i].CompareTo(statement.Iterators[i]);
                if (result != 0)
                {
                    return result;
                }
            }

            result = Statement.CompareTo(statement.Statement);
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
                foreach (var initializer in Initializers)
                {
                    result.Add(initializer);
                }
                result.Add(Condition);
                foreach (var iterator in Iterators)
                {
                    result.Add(iterator);
                }
                result.Add(Statement);
                return result;
            }
        }

        [IgnoreMember]
        public override IEnumerable<Node> AllDescendants
        {
            get
            {
                var result = new List<Node>();
                foreach (var initializer in Initializers)
                {
                    result.Add(initializer);
                    result.AddRange(initializer.AllDescendants);
                }
                result.Add(Condition);
                result.AddRange(Condition.AllDescendants);
                foreach (var iterator in Iterators)
                {
                    result.Add(iterator);
                    result.AddRange(iterator.AllDescendants);
                }
                result.Add(Statement);
                result.AddRange(Statement.AllDescendants);
                return result;
            }
        }

        public override string ToString()
        {
            return $"for ({(string.Join(" ", Initializers))} {Condition}; {(string.Join(" ", Iterators))}) {Statement}";
        }
    }
}
