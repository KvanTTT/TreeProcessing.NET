﻿using ProtoBuf;
using System.Collections.Generic;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.ExpressionStatement)]
    [ProtoContract]
    public class ExpressionStatement : Statement
    {
        public override NodeType NodeType => NodeType.ExpressionStatement;

        [ProtoMember(1)]
        public Expression Expression { get; set; }

        public ExpressionStatement(Expression expression)
        {
            Expression = expression;
        }

        public ExpressionStatement()
        {
        }

        public override int CompareTo(Node other)
        {
            int result = base.CompareTo(other);
            if (result != 0)
            {
                return result;
            }

            ExpressionStatement statement = (ExpressionStatement)other;
            result = Expression.CompareTo(statement.Expression);
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
                result.Add(Expression);
                result.AddRange(Expression.Descendants);
                return result;
            }
        }

        public override string ToString()
        {
            return Expression + ";";
        }
    }
}
