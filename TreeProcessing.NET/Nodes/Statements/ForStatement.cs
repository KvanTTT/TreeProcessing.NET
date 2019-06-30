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

        [DataMember, ProtoMember(1), Key(0)]
        public List<Statement> Initializers { get; set; }

        [DataMember, ProtoMember(2), Key(1)]
        public Expression Condition { get; set; }

        [DataMember, ProtoMember(3), Key(2)]
        public List<Expression> Iterators { get; set; }

        [DataMember, ProtoMember(4), Key(3)]
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

        public override IEnumerable<Node> GetAllDescendants()
        {
            foreach (var initializer in Initializers)
            {
                yield return initializer;

                foreach (var descendant in initializer.GetAllDescendants())
                {
                    yield return descendant;
                }
            }

            yield return Condition;

            foreach (var descendant in Condition.GetAllDescendants())
            {
                yield return descendant;
            }

            foreach (var iterator in Iterators)
            {
                yield return iterator;

                foreach (var descendant in iterator.GetAllDescendants())
                {
                    yield return descendant;
                }
            }

            yield return Statement;

            foreach (var descendant in Statement.GetAllDescendants())
            {
                yield return descendant;
            }
        }

        public override string ToString()
        {
            return $"for ({(string.Join(" ", Initializers))} {Condition}; {(string.Join(" ", Iterators))}) {Statement}";
        }

        public override TResult Accept<TResult>(IVisitor<TResult> nodeVisitor)
        {
            return nodeVisitor.Visit(this);
        }
    }
}
