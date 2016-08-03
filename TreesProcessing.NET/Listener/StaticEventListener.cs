using System;

namespace TreesProcessing.NET
{
    public class StaticEventListener : IEventListener
    {
        public event EventHandler<BinaryOperatorExpression> EnterBinaryOperatorExpression;
        public event EventHandler<BlockStatement> EnterBlockStatement;
        public event EventHandler<BooleanLiteral> EnterBooleanLiteral;
        public event EventHandler<Expression> EnterExpression;
        public event EventHandler<ExpressionStatement> EnterExpressionStatement;
        public event EventHandler<FloatLiteral> EnterFloatLiteral;
        public event EventHandler<ForStatement> EnterForStatement;
        public event EventHandler<Identifier> EnterIdentifier;
        public event EventHandler<IfElseStatement> EnterIfElseStatement;
        public event EventHandler<IntegerLiteral> EnterIntegerLiteral;
        public event EventHandler<InvocationExpression> EnterInvocationExpression;
        public event EventHandler<MemberReferenceExpression> EnterMemberReferenceExpression;
        public event EventHandler<Node> EnterNode;
        public event EventHandler<NullLiteral> EnterNullLiteral;
        public event EventHandler<Statement> EnterStatement;
        public event EventHandler<StringLiteral> EnterStringLiteral;
        public event EventHandler<Terminal> EnterTerminal;
        public event EventHandler<UnaryOperatorExpression> EnterUnaryOperatorExpression;
        public event EventHandler<BinaryOperatorExpression> ExitBinaryOperatorExpression;
        public event EventHandler<BlockStatement> ExitBlockStatement;
        public event EventHandler<BooleanLiteral> ExitBooleanLiteral;
        public event EventHandler<Expression> ExitExpression;
        public event EventHandler<ExpressionStatement> ExitExpressionStatement;
        public event EventHandler<FloatLiteral> ExitFloatLiteral;
        public event EventHandler<ForStatement> ExitForStatement;
        public event EventHandler<Identifier> ExitIdentifier;
        public event EventHandler<IfElseStatement> ExitIfElseStatement;
        public event EventHandler<IntegerLiteral> ExitIntegerLiteral;
        public event EventHandler<InvocationExpression> ExitInvocationExpression;
        public event EventHandler<MemberReferenceExpression> ExitMemberReferenceExpression;
        public event EventHandler<Node> ExitNode;
        public event EventHandler<NullLiteral> ExitNullLiteral;
        public event EventHandler<Statement> ExitStatement;
        public event EventHandler<StringLiteral> ExitStringLiteral;
        public event EventHandler<Terminal> ExitTerminal;
        public event EventHandler<UnaryOperatorExpression> ExitUnaryOperatorExpression;

        public StaticEventListener()
        {
        }

        public void Walk(Node node)
        {
            EnterNode?.Invoke(this, node);
            Visit(node);
            ExitNode?.Invoke(this, node);
        }

        private void Visit(Node node)
        {
            Expression expression;
            Terminal terminal;
            Statement statement;
            if ((expression = node as Expression) != null)
            {
                EnterExpression?.Invoke(this, expression);
                Visit(expression);
                ExitExpression?.Invoke(this, expression);
            }
            else if ((terminal = node as Terminal) != null)
            {
                EnterTerminal?.Invoke(this, terminal);
                Visit(terminal);
                ExitTerminal?.Invoke(this, terminal);
            }
            else if ((statement = node as Statement) != null)
            {
                EnterStatement?.Invoke(this, statement);
                Visit(statement);
                ExitStatement?.Invoke(this, statement);
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
                    EnterBinaryOperatorExpression?.Invoke(this, binaryOperatorExpression);
                    Visit(binaryOperatorExpression);
                    ExitBinaryOperatorExpression?.Invoke(this, binaryOperatorExpression);
                    break;
                case NodeType.InvocationExpression:
                    var invocationExpression = (InvocationExpression)expression;
                    EnterInvocationExpression?.Invoke(this, invocationExpression);
                    Visit(invocationExpression);
                    ExitInvocationExpression?.Invoke(this, invocationExpression);
                    break;
                case NodeType.MemberReferenceExpression:
                    var memberReferenceExpression = (MemberReferenceExpression)expression;
                    EnterMemberReferenceExpression?.Invoke(this, memberReferenceExpression);
                    Visit(memberReferenceExpression);
                    ExitMemberReferenceExpression?.Invoke(this, memberReferenceExpression);
                    break;
                case NodeType.UnaryOperatorExpression:
                    var unaryOperatorExpression = (UnaryOperatorExpression)expression;
                    EnterUnaryOperatorExpression?.Invoke(this, unaryOperatorExpression);
                    Visit(unaryOperatorExpression);
                    ExitUnaryOperatorExpression?.Invoke(this, unaryOperatorExpression);
                    break;
            }
            Terminal terminal;
            if ((terminal = expression as Terminal) != null)
            {
                EnterTerminal?.Invoke(this, terminal);
                Visit(terminal);
                ExitTerminal?.Invoke(this, terminal);
            }
        }

        private void Visit(Terminal terminal)
        {
            switch (terminal.NodeType)
            {
                case NodeType.Identifier:
                    var identifier = (Identifier)terminal;
                    EnterIdentifier?.Invoke(this, identifier);
                    ExitIdentifier?.Invoke(this, identifier);
                    break;
                case NodeType.BooleanLiteral:
                    var booleanLiteral = (BooleanLiteral)terminal;
                    EnterBooleanLiteral?.Invoke(this, booleanLiteral);
                    ExitBooleanLiteral?.Invoke(this, booleanLiteral);
                    break;
                case NodeType.FloatLiteral:
                    var floatLiteral = (FloatLiteral)terminal;
                    EnterFloatLiteral?.Invoke(this, floatLiteral);
                    ExitFloatLiteral?.Invoke(this, floatLiteral);
                    break;
                case NodeType.IntegerLiteral:
                    var integerLiteral = (IntegerLiteral)terminal;
                    EnterIntegerLiteral?.Invoke(this, integerLiteral);
                    ExitIntegerLiteral?.Invoke(this, integerLiteral);
                    break;
                case NodeType.NullLiteral:
                    var nullLiteral = (NullLiteral)terminal;
                    EnterNullLiteral?.Invoke(this, nullLiteral);
                    ExitNullLiteral?.Invoke(this, nullLiteral);
                    break;
                case NodeType.StringLiteral:
                    var stringLiteral = (StringLiteral)terminal;
                    EnterStringLiteral?.Invoke(this, stringLiteral);
                    ExitStringLiteral?.Invoke(this, stringLiteral);
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
                    var blockStatement = (BlockStatement)statement;
                    EnterBlockStatement?.Invoke(this, blockStatement);
                    Visit(blockStatement);
                    ExitBlockStatement?.Invoke(this, blockStatement);
                    break;
                case NodeType.ExpressionStatement:
                    var expressionStatement = (ExpressionStatement)statement;
                    EnterExpressionStatement?.Invoke(this, expressionStatement);
                    Visit(expressionStatement);
                    ExitExpressionStatement?.Invoke(this, expressionStatement);
                    break;
                case NodeType.ForStatement:
                    var forStatement = (ForStatement)statement;
                    EnterForStatement?.Invoke(this, forStatement);
                    Visit(forStatement);
                    ExitForStatement?.Invoke(this, forStatement);
                    break;
                case NodeType.IfElseStatement:
                    var ifElseStatement = (IfElseStatement)statement;
                    EnterIfElseStatement?.Invoke(this, ifElseStatement);
                    Visit(ifElseStatement);
                    ExitIfElseStatement?.Invoke(this, ifElseStatement);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private void Visit(BinaryOperatorExpression binaryOperatorExpression)
        {
            EnterExpression?.Invoke(this, binaryOperatorExpression.Left);
            Visit( binaryOperatorExpression.Left);
            ExitExpression?.Invoke(this, binaryOperatorExpression.Left);
            EnterExpression?.Invoke(this, binaryOperatorExpression.Right);
            Visit( binaryOperatorExpression.Right);
            ExitExpression?.Invoke(this, binaryOperatorExpression.Right);
        }

        private void Visit(InvocationExpression invocationExpression)
        {
            EnterExpression?.Invoke(this, invocationExpression.Target);
            Visit(invocationExpression.Target);
            ExitExpression?.Invoke(this, invocationExpression.Target);
            foreach (var arg in invocationExpression.Args)
            {
                EnterExpression?.Invoke(this, arg);
                Visit(arg);
                ExitExpression?.Invoke(this, arg);
            }
        }

        private void Visit(MemberReferenceExpression memberReferenceExpression)
        {
            EnterExpression?.Invoke(this, memberReferenceExpression.Target);
            Visit(memberReferenceExpression.Target);
            ExitExpression?.Invoke(this, memberReferenceExpression.Target);
            EnterIdentifier?.Invoke(this, memberReferenceExpression.Name);
            Visit(memberReferenceExpression.Name);
            ExitIdentifier?.Invoke(this, memberReferenceExpression.Name);
        }

        private void Visit(UnaryOperatorExpression unaryOperatorExpression)
        {
            EnterExpression?.Invoke(this, unaryOperatorExpression.Expression);
            Visit(unaryOperatorExpression.Expression);
            ExitExpression?.Invoke(this, unaryOperatorExpression.Expression);
        }

        private void Visit(BlockStatement blockStatement)
        {
            foreach (var statement in blockStatement.Statements)
            {
                EnterStatement?.Invoke(this, statement);
                Visit(statement);
                ExitStatement?.Invoke(this, statement);
            }
        }

        private void Visit(ExpressionStatement expressionStatement)
        {
            EnterExpression?.Invoke(this, expressionStatement.Expression);
            Visit(expressionStatement.Expression);
            ExitExpression?.Invoke(this, expressionStatement.Expression);
        }

        private void Visit(ForStatement forStatement)
        {
            foreach (var initializer in forStatement.Initializers)
            {
                EnterStatement?.Invoke(this, initializer);
                Visit(initializer);
                ExitStatement?.Invoke(this, initializer);
            }
            EnterExpression?.Invoke(this, forStatement.Condition);
            Visit(forStatement.Condition);
            ExitExpression?.Invoke(this, forStatement.Condition);
            foreach (var iterator in forStatement.Iterators)
            {
                EnterExpression?.Invoke(this, iterator);
                Visit(iterator);
                ExitExpression?.Invoke(this, iterator);
            }
            EnterStatement?.Invoke(this, forStatement.Statement);
            Visit(forStatement.Statement);
            ExitStatement?.Invoke(this, forStatement.Statement);
        }

        private void Visit(IfElseStatement ifElseStatement)
        {
            EnterExpression?.Invoke(this, ifElseStatement.Condition);
            Visit(ifElseStatement.Condition);
            ExitExpression?.Invoke(this, ifElseStatement.Condition);
            EnterStatement?.Invoke(this, ifElseStatement.TrueStatement);
            Visit(ifElseStatement.TrueStatement);
            ExitStatement?.Invoke(this, ifElseStatement.TrueStatement);
            if (ifElseStatement.FalseStatement != null)
            {
                EnterStatement?.Invoke(this, ifElseStatement.FalseStatement);
                Visit(ifElseStatement.FalseStatement);
                ExitStatement?.Invoke(this, ifElseStatement.FalseStatement);
            }
        }
    }
}
