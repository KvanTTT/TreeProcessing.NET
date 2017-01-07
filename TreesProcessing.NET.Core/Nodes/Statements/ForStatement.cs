using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.ForStatement)]
#if !CORE
    [Serializable]
#endif
    [DataContract]
    [ProtoContract]
    public class ForStatement : Statement
    {
        public override NodeType NodeType => NodeType.ForStatement;

        [DataMember]
        [ProtoMember(1)]
        public List<Statement> Initializers { get; set; }

        [DataMember]
        [ProtoMember(2)]
        public Expression Condition { get; set; }

        [DataMember]
        [ProtoMember(3)]
        public List<Expression> Iterators { get; set; }

        [DataMember]
        [ProtoMember(4)]
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

        public override IEnumerable<Node> Descendants
        {
            get
            {
                var result = new List<Node>();
                foreach (var initializer in Initializers)
                {
                    result.Add(initializer);
                    result.AddRange(initializer.Descendants);
                }
                result.Add(Condition);
                result.AddRange(Condition.Descendants);
                foreach (var iterator in Iterators)
                {
                    result.Add(iterator);
                    result.AddRange(iterator.Descendants);
                }
                result.Add(Statement);
                result.AddRange(Statement.Descendants);
                return result;
            }
        }

        public override string ToString()
        {
            return $"for ({(string.Join(" ", Initializers))} {Condition}; {(string.Join(" ", Iterators))}) {Statement}";
        }
    }
}
