using System;

namespace TreeProcessing.NET
{
    public class StaticListener : IListener
    {
        public StaticListener()
        {
        }

        public void Walk(Node node)
        {
            Enter(node);
            Visit(node);
            Exit(node);
        }

        public virtual void Enter(Terminal terminal)
        {
        }

        public virtual void Enter(Statement statement)
        {
        }

        public virtual void Enter(Expression expression)
        {
        }

        public virtual void Exit(Expression exrpession)
        {
        }

        public virtual void Enter(BinaryOperatorExpression binaryOperatorExpression)
        {
        }

        public virtual void Enter(MemberReferenceExpression memberReferenceExpression)
        {
        }

        public virtual void Enter(UnaryOperatorExpression unaryOperatorExpression)
        {
        }

        public virtual void Enter(InvocationExpression invocationExpression)
        {
        }

        public virtual void Enter(BooleanLiteral unaryOperatorExpression)
        {
        }

        public virtual void Enter(IntegerLiteral integerLiteral)
        {
        }

        public virtual void Enter(StringLiteral stringLiteral)
        {
        }

        public virtual void Enter(BlockStatement blockStatement)
        {
        }

        public virtual void Enter(ForStatement forStatement)
        {
        }

        public virtual void Enter(IfElseStatement ifElseStatement)
        {
        }

        public virtual void Enter(ExpressionStatement expressionStatement)
        {
        }

        public virtual void Enter(Identifier identifier)
        {
        }

        public virtual void Enter(NullLiteral nullLiteral)
        {
        }

        public virtual void Enter(FloatLiteral floatLiteral)
        {
        }

        public virtual void Enter(Node node)
        {
        }

        public virtual void Exit(BlockStatement blockStatement)
        {
        }

        public virtual void Exit(ExpressionStatement expressionStatement)
        {
        }

        public virtual void Exit(ForStatement forStatement)
        {
        }

        public virtual void Exit(IfElseStatement ifElseStatement)
        {
        }

        public virtual void Exit(BinaryOperatorExpression binaryOperatorExpression)
        {
        }

        public virtual void Exit(MemberReferenceExpression memberReferenceExpression)
        {
        }

        public virtual void Exit(UnaryOperatorExpression unaryOperatorExpression)
        {
        }

        public virtual void Exit(InvocationExpression invocationExpression)
        {
        }

        public virtual void Exit(Statement statement)
        {
        }

        public virtual void Exit(Terminal terminal)
        {
        }

        public virtual void Exit(Node node)
        {
        }

        public virtual void Exit(BooleanLiteral unaryOperatorExpression)
        {
        }

        public virtual void Exit(FloatLiteral floatLiteral)
        {
        }

        public virtual void Exit(IntegerLiteral integerLiteral)
        {
        }

        public virtual void Exit(NullLiteral nullLiteral)
        {
        }

        public virtual void Exit(StringLiteral stringLiteral)
        {
        }

        public virtual void Exit(Identifier identifier)
        {
        }

        private void Visit(Node node)
        {
            Expression expression;
            Terminal terminal;
            Statement statement;
            if ((expression = node as Expression) != null)
            {
                Enter(expression);
                Visit(expression);
                Exit(expression);
            }
            else if ((terminal = node as Terminal) != null)
            {
                Enter(terminal);
                Visit(terminal);
                Exit(terminal);
            }
            else if ((statement = node as Statement) != null)
            {
                Enter(statement);
                Visit(statement);
                Exit(statement);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private void Visit(Expression expression)
        {
            switch (expression.NodeType)
            {
                case NodeType.BinaryOperatorExpression:
                    var binaryOperatorExpression = (BinaryOperatorExpression)expression;
                    Enter(binaryOperatorExpression);
                    Visit(binaryOperatorExpression);
                    Exit(binaryOperatorExpression);
                    break;
                case NodeType.InvocationExpression:
                    var invocationExpression = (InvocationExpression)expression;
                    Enter(invocationExpression);
                    Visit(invocationExpression);
                    Exit(invocationExpression);
                    break;
                case NodeType.MemberReferenceExpression:
                    var memberReferenceExpression = (MemberReferenceExpression)expression;
                    Enter(memberReferenceExpression);
                    Visit(memberReferenceExpression);
                    Exit(memberReferenceExpression);
                    break;
                case NodeType.UnaryOperatorExpression:
                    var unaryOperatorExpression = (UnaryOperatorExpression)expression;
                    Enter(unaryOperatorExpression);
                    Visit(unaryOperatorExpression);
                    Exit(unaryOperatorExpression);
                    break;
            }
            Terminal terminal;
            if ((terminal = expression as Terminal) != null)
            {
                Enter(terminal);
                Visit(terminal);
                Exit(terminal);
            }
        }

        private void Visit(Terminal terminal)
        {
            switch (terminal.NodeType)
            {
                case NodeType.Identifier:
                    var identifier = (Identifier)terminal;
                    Enter(identifier);
                    Exit(identifier);
                    break;
                case NodeType.BooleanLiteral:
                    var booleanLiteral = (BooleanLiteral)terminal;
                    Enter(booleanLiteral);
                    Exit(booleanLiteral);
                    break;
                case NodeType.FloatLiteral:
                    var floatLiteral = (FloatLiteral)terminal;
                    Enter(floatLiteral);
                    Exit(floatLiteral);
                    break;
                case NodeType.IntegerLiteral:
                    var integerLiteral = (IntegerLiteral)terminal;
                    Enter(integerLiteral);
                    Exit(integerLiteral);
                    break;
                case NodeType.NullLiteral:
                    var nullLiteral = (NullLiteral)terminal;
                    Enter(nullLiteral);
                    Exit(nullLiteral);
                    break;
                case NodeType.StringLiteral:
                    var stringLiteral = (StringLiteral)terminal;
                    Enter(stringLiteral);
                    Exit(stringLiteral);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private void Visit(Statement statement)
        {
            switch (statement.NodeType)
            {
                case NodeType.BlockStatement:
                    Enter((BlockStatement)statement);
                    Visit((BlockStatement)statement);
                    Exit((BlockStatement)statement);
                    break;
                case NodeType.ExpressionStatement:
                    Enter((ExpressionStatement)statement);
                    Visit((ExpressionStatement)statement);
                    Exit((ExpressionStatement)statement);
                    break;
                case NodeType.ForStatement:
                    Enter((ForStatement)statement);
                    Visit((ForStatement)statement);
                    Exit((ForStatement)statement);
                    break;
                case NodeType.IfElseStatement:
                    Enter((IfElseStatement)statement);
                    Visit((IfElseStatement)statement);
                    Exit((IfElseStatement)statement);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private void Visit(BinaryOperatorExpression binaryOperatorExpression)
        {
            Enter(binaryOperatorExpression.Left);
            Visit(binaryOperatorExpression.Left);
            Exit(binaryOperatorExpression.Left);
            Enter(binaryOperatorExpression.Right);
            Visit(binaryOperatorExpression.Right);
            Exit(binaryOperatorExpression.Right);
        }

        private void Visit(InvocationExpression invocationExpression)
        {
            Enter(invocationExpression.Target);
            Visit(invocationExpression.Target);
            Exit(invocationExpression.Target);
            foreach (var arg in invocationExpression.Args)
            {
                Enter(arg);
                Visit(arg);
                Exit(arg);
            }
        }

        private void Visit(MemberReferenceExpression memberReferenceExpression)
        {
            Enter(memberReferenceExpression.Target);
            Visit(memberReferenceExpression.Target);
            Exit(memberReferenceExpression.Target);
            Enter(memberReferenceExpression.Name);
            Visit(memberReferenceExpression.Name);
            Exit(memberReferenceExpression.Name);
        }

        private void Visit(UnaryOperatorExpression unaryOperatorExpression)
        {
            Enter(unaryOperatorExpression.Expression);
            Visit(unaryOperatorExpression.Expression);
            Exit(unaryOperatorExpression.Expression);
        }

        private void Visit(BlockStatement blockStatement)
        {
            foreach (var statement in blockStatement.Statements)
            {
                Enter(statement);
                Visit(statement);
                Exit(statement);
            }
        }

        private void Visit(ExpressionStatement expressionStatement)
        {
            Enter(expressionStatement.Expression);
            Visit(expressionStatement.Expression);
            Exit(expressionStatement.Expression);
        }

        private void Visit(ForStatement forStatement)
        {
            foreach (var initializer in forStatement.Initializers)
            {
                Enter(initializer);
                Visit(initializer);
                Exit(initializer);
            }
            Enter(forStatement.Condition);
            Visit(forStatement.Condition);
            Exit(forStatement.Condition);
            foreach (var iterator in forStatement.Iterators)
            {
                Enter(iterator);
                Visit(iterator);
                Exit(iterator);
            }
            Enter(forStatement.Statement);
            Visit(forStatement.Statement);
            Exit(forStatement.Statement);
        }

        private void Visit(IfElseStatement ifElseStatement)
        {
            Enter(ifElseStatement.Condition);
            Visit(ifElseStatement.Condition);
            Exit(ifElseStatement.Condition);
            Enter(ifElseStatement.TrueStatement);
            Visit(ifElseStatement.TrueStatement);
            Exit(ifElseStatement.TrueStatement);
            if (ifElseStatement.FalseStatement != null)
            {
                Enter(ifElseStatement.FalseStatement);
                Visit(ifElseStatement.FalseStatement);
                Exit(ifElseStatement.FalseStatement);
            }
        }
    }
}
