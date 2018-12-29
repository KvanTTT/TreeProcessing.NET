using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeProcessing.NET
{
    public class StaticCloner : StaticVisitor<Node>
    {
        public override Node Visit(Token terminal)
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

        public override Node Visit(Statement statement)
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

        public override Node Visit(Identifier identifier)
        {
            return new Identifier(identifier.Id);
        }

        public override Node Visit(Expression expression)
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

            if (expression is Token token)
            {
                return Visit(token);
            }

            throw new InvalidOperationException();
        }

        public override Node Visit(Node node)
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

        public override Node Visit(BinaryOperatorExpression expression)
        {
            return new BinaryOperatorExpression(
                (Expression)Visit(expression.Left), expression.Operator, (Expression)Visit(expression.Right));
        }

        public override Node Visit(BlockStatement blockStatement)
        {
            List<Statement> statements = blockStatement.Statements.Select(s => (Statement)Visit(s)).ToList();
            return new BlockStatement(statements);
        }

        public override Node Visit(BooleanLiteral unaryOperatorExpression)
        {
            return new BooleanLiteral(unaryOperatorExpression.Value);
        }

        public override Node Visit(ExpressionStatement expressionStatement)
        {
            return new ExpressionStatement((Expression)Visit(expressionStatement.Expression));
        }

        public override Node Visit(FloatLiteral floatLiteral)
        {
            return new FloatLiteral(floatLiteral.Value);
        }

        public override Node Visit(ForStatement forStatement)
        {
            List<Statement> initializers = forStatement.Initializers.Select(init => (Statement)Visit(init)).ToList();
            Expression condition = (Expression)Visit(forStatement.Condition);
            List<Expression> iterators = forStatement.Iterators.Select(iter => (Expression)Visit(iter)).ToList();
            Statement statement = (Statement)Visit(forStatement.Statement);
            return new ForStatement(initializers, condition, iterators, statement);
        }

        public override Node Visit(IfElseStatement ifElseStatement)
        {
            return new IfElseStatement((Expression)Visit(ifElseStatement.Condition),
                (Statement)Visit(ifElseStatement.TrueStatement),
                ifElseStatement.FalseStatement != null ? (Statement)Visit(ifElseStatement.FalseStatement) : null);
        }

        public override Node Visit(IntegerLiteral integerLiteral)
        {
            return new IntegerLiteral(integerLiteral.Value);
        }

        public override Node Visit(InvocationExpression invocationExpression)
        {
            List<Expression> args = invocationExpression.Args.Select(arg => (Expression)Visit(arg)).ToList();
            return new InvocationExpression((Expression)Visit(invocationExpression.Target), args);
        }

        public override Node Visit(MemberReferenceExpression memberReferenceExpression)
        {
            return new MemberReferenceExpression(
                (Expression)Visit(memberReferenceExpression.Target),
                (Identifier)Visit(memberReferenceExpression.Name));
        }

        public override Node Visit(NullLiteral nullLiteral)
        {
            return new NullLiteral();
        }

        public override Node Visit(StringLiteral stringLiteral)
        {
            return new StringLiteral(stringLiteral.Value);
        }

        public override Node Visit(UnaryOperatorExpression unaryOperatorExpression)
        {
            return new UnaryOperatorExpression(unaryOperatorExpression.Operator,
                (Expression)Visit(unaryOperatorExpression.Expression));
        }
    }
}
