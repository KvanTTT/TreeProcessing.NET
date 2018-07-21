using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeProcessing.NET
{
    public class StaticCloner : StaticVisitor<Node>
    {
        public override Node Visit(Token terminal)
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

        public override Node Visit(Statement statement)
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

        public override Node Visit(Identifier identifier)
        {
            return new Identifier(identifier.Id);
        }

        public override Node Visit(Expression expression)
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

        public override Node Visit(Node node)
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
