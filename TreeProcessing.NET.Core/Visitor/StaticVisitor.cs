using System;

namespace TreeProcessing.NET
{
    public class StaticVisitor<T> : IVisitor<T>
    {
        public virtual T Visit(Token terminal)
        {
            switch (terminal.NodeType)
            {
                case NodeType.Identifier:
                    return Visit((Identifier)terminal);
                case NodeType.BooleanLiteral:
                    return Visit((BooleanLiteral)terminal);
                case NodeType.FloatLiteral:
                    return Visit((FloatLiteral)terminal);
                case NodeType.IntegerLiteral:
                    return Visit((IntegerLiteral)terminal);
                case NodeType.NullLiteral:
                    return Visit((NullLiteral)terminal);
                case NodeType.StringLiteral:
                    return Visit((StringLiteral)terminal);
                default:
                    throw new InvalidOperationException();
            }
        }

        public virtual T Visit(Statement statement)
        {
            switch (statement.NodeType)
            {
                case NodeType.BlockStatement:
                    return Visit((BlockStatement)statement);
                case NodeType.ExpressionStatement:
                    return Visit((ExpressionStatement)statement);
                case NodeType.ForStatement:
                    return Visit((ForStatement)statement);
                case NodeType.IfElseStatement:
                    return Visit((IfElseStatement)statement);
                default:
                    throw new InvalidOperationException();
            }
        }

        public virtual T Visit(Identifier identifier)
        {
            return DefaultResult;
        }

        public virtual T Visit(Expression expression)
        {
            switch (expression.NodeType)
            {
                case NodeType.BinaryOperatorExpression:
                    return Visit((BinaryOperatorExpression)expression);
                case NodeType.InvocationExpression:
                    return Visit((InvocationExpression)expression);
                case NodeType.MemberReferenceExpression:
                    return Visit((MemberReferenceExpression)expression);
                case NodeType.UnaryOperatorExpression:
                    return Visit((UnaryOperatorExpression)expression);
            }
            if (expression is Token)
            {
                return Visit((Token)expression);
            }

            throw new InvalidOperationException();
        }

        public virtual T Visit(Node node)
        {
            if (node is Expression)
            {
                return Visit((Expression)node);
            }
            else if (node is Token)
            {
                return Visit((Token)node);
            }
            else if (node is Statement)
            {
                return Visit((Statement)node);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public virtual T Visit(BinaryOperatorExpression expression)
        {
            Visit(expression.Left);
            Visit(expression.Right);

            return DefaultResult;
        }

        public virtual T Visit(BlockStatement blockStatement)
        {
            foreach (Statement statement in blockStatement.Statements)
            {
                Visit(statement);
            }
            return DefaultResult;
        }

        public virtual T Visit(BooleanLiteral unaryOperatorExpression)
        {
            return DefaultResult;
        }

        public virtual T Visit(ExpressionStatement expressionStatement)
        {
            Visit(expressionStatement.Expression);

            return DefaultResult;
        }

        public virtual T Visit(FloatLiteral floatLiteral)
        {
            return DefaultResult;
        }

        public virtual T Visit(ForStatement forStatement)
        {
            foreach (Statement initializer in forStatement.Initializers)
            {
                Visit(initializer);
            }
            Visit(forStatement.Condition);
            foreach (Expression iterator in forStatement.Iterators)
            {
                Visit(iterator);
            }
            Visit(forStatement.Statement);

            return DefaultResult;
        }

        public virtual T Visit(IfElseStatement ifElseStatement)
        {
            Visit(ifElseStatement.Condition);
            Visit(ifElseStatement.TrueStatement);
            if (ifElseStatement.FalseStatement != null)
            {
                Visit(ifElseStatement.FalseStatement);
            }

            return DefaultResult;
        }

        public virtual T Visit(IntegerLiteral integerLiteral)
        {
            return DefaultResult;
        }

        public virtual T Visit(InvocationExpression invocationExpression)
        {
            foreach (Expression arg in invocationExpression.Args)
            {
                Visit(arg);
            }
            Visit(invocationExpression.Target);

            return DefaultResult;
        }

        public virtual T Visit(MemberReferenceExpression memberReferenceExpression)
        {
            Visit(memberReferenceExpression.Target);
            Visit(memberReferenceExpression.Name);

            return DefaultResult;
        }

        public virtual T Visit(NullLiteral nullLiteral)
        {
            return DefaultResult;
        }

        public virtual T Visit(StringLiteral stringLiteral)
        {
            return DefaultResult;
        }

        public virtual T Visit(UnaryOperatorExpression unaryOperatorExpression)
        {
            Visit(unaryOperatorExpression.Expression);
            return DefaultResult;
        }

        public virtual T DefaultResult => default(T);
    }
}
