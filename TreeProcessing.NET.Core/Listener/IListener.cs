using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeProcessing.NET
{
    public interface IListener
    {
        void Walk(Node node);

        #region Abstract

        void Enter(Node node);

        void Enter(Expression expression);

        void Enter(Token terminal);

        void Enter(Statement statement);

        void Exit(Expression expression);

        void Exit(Token terminal);

        void Exit(Node node);

        void Exit(Statement statement);

        #endregion

        #region Expressions

        void Enter(BinaryOperatorExpression binaryOperatorExpression);

        void Enter(InvocationExpression invocationExpression);

        void Enter(MemberReferenceExpression memberReferenceExpression);

        void Enter(UnaryOperatorExpression unaryOperatorExpression);

        void Exit(BinaryOperatorExpression binaryOperatorExpression);

        void Exit(InvocationExpression invocationExpression);

        void Exit(MemberReferenceExpression memberReferenceExpression);

        void Exit(UnaryOperatorExpression unaryOperatorExpression);

        #endregion

        #region Literals

        void Enter(BooleanLiteral unaryOperatorExpression);

        void Enter(FloatLiteral floatLiteral);

        void Enter(IntegerLiteral integerLiteral);

        void Enter(NullLiteral nullLiteral);

        void Enter(StringLiteral stringLiteral);

        void Enter(Identifier identifier);

        void Exit(BooleanLiteral unaryOperatorExpression);

        void Exit(FloatLiteral floatLiteral);

        void Exit(IntegerLiteral integerLiteral);

        void Exit(NullLiteral nullLiteral);

        void Exit(StringLiteral stringLiteral);

        void Exit(Identifier identifier);

        #endregion

        #region Statemenets

        void Enter(BlockStatement blockStatement);

        void Enter(ExpressionStatement expressionStatement);

        void Enter(ForStatement forStatement);

        void Enter(IfElseStatement ifElseStatement);

        void Exit(BlockStatement blockStatement);

        void Exit(ExpressionStatement expressionStatement);

        void Exit(ForStatement forStatement);

        void Exit(IfElseStatement ifElseStatement);

        #endregion
    }
}
