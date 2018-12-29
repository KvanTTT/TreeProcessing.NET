namespace TreeProcessing.NET
{
    public enum NodeType
    {
        Node = 0,
        Expression = 1,
        Statement = 2,
        Token = 3,

        BinaryOperatorExpression = 10,
        InvocationExpression = 11,
        MemberReferenceExpression = 12,
        UnaryOperatorExpression = 13,

        Identifier = 20,
        BooleanLiteral = 21,
        FloatLiteral = 22,
        IntegerLiteral = 23,
        NullLiteral = 24,
        StringLiteral = 25,
        
        BlockStatement = 30,
        ExpressionStatement = 31,
        ForStatement = 32,
        IfElseStatement = 33
    }
}
