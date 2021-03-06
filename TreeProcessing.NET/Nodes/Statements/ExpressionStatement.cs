﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MessagePack;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.ExpressionStatement)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    [MessagePackObject]
    public class ExpressionStatement : Statement
    {
        [IgnoreMember]
        public override NodeType NodeType => NodeType.ExpressionStatement;

        [DataMember, ProtoMember(1), Key(0)]
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

            ExpressionStatement statement = (ExpressionStatement) other;
            result = Expression.CompareTo(statement.Expression);
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
                result.Add(Expression);
                return result;
            }
        }

        public override IEnumerable<Node> GetAllDescendants()
        {
            yield return Expression;

            foreach (var expr in Expression.GetAllDescendants())
            {
                yield return expr;
            }
        }

        public override string ToString()
        {
            return Expression + ";";
        }

        public override TResult Accept<TResult>(IVisitor<TResult> nodeVisitor)
        {
            return nodeVisitor.Visit(this);
        }
    }
}
