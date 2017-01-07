using System;

namespace TreesProcessing.NET
{
    public interface IEventListener
    {
        void Walk(Node node);

        #region Abstract

        event EventHandler<Node> EnterNode;

        event EventHandler<Node> ExitNode;

        event EventHandler<Expression> EnterExpression;

        event EventHandler<Expression> ExitExpression;

        event EventHandler<Terminal> EnterTerminal;

        event EventHandler<Terminal> ExitTerminal;

        event EventHandler<Statement> EnterStatement;

        event EventHandler<Statement> ExitStatement;

        #endregion

        #region Expressions

        event EventHandler<BinaryOperatorExpression> EnterBinaryOperatorExpression;

        event EventHandler<BinaryOperatorExpression> ExitBinaryOperatorExpression;

        event EventHandler<InvocationExpression> EnterInvocationExpression;

        event EventHandler<InvocationExpression> ExitInvocationExpression;

        event EventHandler<MemberReferenceExpression> EnterMemberReferenceExpression;

        event EventHandler<MemberReferenceExpression> ExitMemberReferenceExpression;

        event EventHandler<UnaryOperatorExpression> EnterUnaryOperatorExpression;

        event EventHandler<UnaryOperatorExpression> ExitUnaryOperatorExpression;

        #endregion

        #region Literals

        event EventHandler<BooleanLiteral> EnterBooleanLiteral;

        event EventHandler<BooleanLiteral> ExitBooleanLiteral;

        event EventHandler<FloatLiteral> EnterFloatLiteral;

        event EventHandler<FloatLiteral> ExitFloatLiteral;

        event EventHandler<IntegerLiteral> EnterIntegerLiteral;

        event EventHandler<IntegerLiteral> ExitIntegerLiteral;

        event EventHandler<NullLiteral> EnterNullLiteral;

        event EventHandler<NullLiteral> ExitNullLiteral;

        event EventHandler<StringLiteral> EnterStringLiteral;

        event EventHandler<StringLiteral> ExitStringLiteral;

        event EventHandler<Identifier> EnterIdentifier;

        event EventHandler<Identifier> ExitIdentifier;

        #endregion

        #region Statements

        event EventHandler<BlockStatement> EnterBlockStatement;

        event EventHandler<BlockStatement> ExitBlockStatement;

        event EventHandler<ExpressionStatement> EnterExpressionStatement;

        event EventHandler<ExpressionStatement> ExitExpressionStatement;

        event EventHandler<ForStatement> EnterForStatement;

        event EventHandler<ForStatement> ExitForStatement;

        event EventHandler<IfElseStatement> EnterIfElseStatement;

        event EventHandler<IfElseStatement> ExitIfElseStatement;

        #endregion
    }
}
