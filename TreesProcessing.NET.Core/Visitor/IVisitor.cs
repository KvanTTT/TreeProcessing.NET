namespace TreesProcessing.NET
{
    public interface IVisitor<out T>
    {
        #region Abstract

        T Visit(Node node);

        T Visit(Expression expression);

        T Visit(Terminal terminal);

        T Visit(Statement statement);

        #endregion

        #region Expressions

        T Visit(BinaryOperatorExpression binaryOperatorExpression);

        T Visit(InvocationExpression invocationExpression);

        T Visit(MemberReferenceExpression memberReferenceExpression);

        T Visit(UnaryOperatorExpression unaryOperatorExpression);

        #endregion

        #region Literals

        T Visit(BooleanLiteral unaryOperatorExpression);

        T Visit(FloatLiteral floatLiteral);

        T Visit(IntegerLiteral integerLiteral);

        T Visit(NullLiteral nullLiteral);

        T Visit(StringLiteral stringLiteral);

        T Visit(Identifier identifier);

        #endregion

        #region Statements

        T Visit(BlockStatement blockStatement);

        T Visit(ExpressionStatement expressionStatement);

        T Visit(ForStatement forStatement);

        T Visit(IfElseStatement ifElseStatement);

        #endregion
    }
}
