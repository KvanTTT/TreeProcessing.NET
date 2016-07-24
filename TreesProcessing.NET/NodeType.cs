using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET
{
    public enum NodeType
    {
        Node,
        Expression,
        Statement,
        Terminal,

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
