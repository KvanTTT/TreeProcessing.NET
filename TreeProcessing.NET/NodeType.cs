namespace TreeProcessing.NET
{
    public enum NodeType
    {
        Node,
        Expression,
        Statement,
        Token,

        BinaryOperatorExpression,
        InvocationExpression,
        MemberReferenceExpression,
        UnaryOperatorExpression,

        BlockStatement,
        ExpressionStatement,
        ForStatement,
        IfElseStatement,

        Identifier,
        BooleanLiteral,
        FloatLiteral,
        IntegerLiteral,
        NullLiteral,
        StringLiteral
    }
}
