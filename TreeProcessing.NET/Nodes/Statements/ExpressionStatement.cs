﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.ExpressionStatement)]
#if PORTABLE || NET
    [Serializable]
#endif
    [DataContract]
    [ProtoContract]
    public class ExpressionStatement : Statement
    {
        public override NodeType NodeType => NodeType.ExpressionStatement;

        [DataMember]
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

        public override IEnumerable<Node> Children
        {
            get
            {
                var result = new List<Node>();
                result.Add(Expression);
                return result;
            }
        }

        public override IEnumerable<Node> AllDescendants
        {
            get
            {
                var result = new List<Node>();
                result.Add(Expression);
                result.AddRange(Expression.AllDescendants);
                return result;
            }
        }

        public override string ToString()
        {
            return Expression + ";";
        }
    }
}