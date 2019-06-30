using System;

namespace TreeProcessing.NET
{
    public class StaticVisitor<T> : IVisitor<T>
    {
        public virtual T Visit(Token terminal)
        {
            switch (terminal)
            {
                case Identifier identifier:
                    return Visit(identifier);
                case BooleanLiteral booleanLiteral:
                    return Visit(booleanLiteral);
                case FloatLiteral floatLiteral:
                    return Visit(floatLiteral);
                case IntegerLiteral integerLiteral:
                    return Visit(integerLiteral);
                case NullLiteral nullLiteral:
                    return Visit(nullLiteral);
                case StringLiteral stringLiteral:
                    return Visit(stringLiteral);
                default:
                    throw new InvalidOperationException();
            }
        }

        public virtual T Visit(Statement statement)
        {
            switch (statement)
            {
                case BlockStatement blockStatement:
                    return Visit(blockStatement);
                case ExpressionStatement expressionStatement:
                    return Visit(expressionStatement);
                case ForStatement forStatement:
                    return Visit(forStatement);
                case IfElseStatement ifElseStatement:
                    return Visit(ifElseStatement);
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
            switch (expression)
            {
                case BinaryOperatorExpression binaryOperatorExpression:
                    return Visit(binaryOperatorExpression);
                case InvocationExpression invocationExpression:
                    return Visit(invocationExpression);
                case MemberReferenceExpression memberReferenceExpression:
                    return Visit(memberReferenceExpression);
                case UnaryOperatorExpression unaryOperatorExpression:
                    return Visit(unaryOperatorExpression);
            }
            if (expression is Token)
            {
                return Visit((Token)expression);
            }

            throw new InvalidOperationException();
        }

        public virtual T Visit(Node node)
        {
            if (node is Token token)
            {
                return Visit(token);
            }

            if (node is Expression expression)
            {
                return Visit(expression);
            }

            if (node is Statement statement)
            {
                return Visit(statement);
            }

            throw new InvalidOperationException();
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
