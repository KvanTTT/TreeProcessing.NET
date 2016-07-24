using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET.Tests
{
    public static class SampleTree
    {
        public static Node Init()
        {
            var result = new BlockStatement
            {
                Statements = new List<Statement>()
                {
                    new ExpressionStatement
                    {
                        Expression = new BinaryOperatorExpression
                        {
                            Left = new Identifier("variable"),
                            Operator = "==",
                            Right = new IntegerLiteral(42)
                        }
                    },
                    new ExpressionStatement
                    {
                        Expression = new InvocationExpression
                        {
                            Target = new MemberReferenceExpression
                            {
                                Target = new MemberReferenceExpression
                                {
                                    Target = new Identifier("b"),
                                    Name = new Identifier("a")
                                },
                                Name = new Identifier("c"),
                            },
                            Args = new List<Expression>()
                            {
                                new BooleanLiteral(true),
                                new StringLiteral("asdf"),
                                new NullLiteral(),
                                new UnaryOperatorExpression
                                {
                                    Operator = "-",
                                    Expression = new FloatLiteral(1234.5678f)
                                }
                            }
                        }
                    },
                    new ForStatement
                    {
                        Initializers = new List<Statement>()
                        {
                            new ExpressionStatement
                            {
                                Expression = new BinaryOperatorExpression
                                {
                                    Left = new Identifier("index"),
                                    Operator = "=",
                                    Right = new IntegerLiteral(0)
                                }
                            }
                        },
                        Condition = new BinaryOperatorExpression
                        {
                            Left = new Identifier("index"),
                            Operator = "<",
                            Right = new IntegerLiteral(100)
                        },
                        Iterators = new List<Expression>()
                        {
                            new UnaryOperatorExpression
                            {
                                Operator = "++",
                                Expression = new Identifier("index")
                            }
                        },
                        Statement = new IfElseStatement
                        {
                            Condition = new BinaryOperatorExpression
                            {
                                Left = new Identifier("index"),
                                Operator = "<",
                                Right = new IntegerLiteral(50)
                            },
                            TrueStatement = new ExpressionStatement
                            {
                                Expression = new InvocationExpression
                                {
                                    Target = new Identifier("print"),
                                    Args = new List<Expression>()
                                }
                            },
                            FalseStatement = null
                        }
                    }
                }
            };
            return result;
        }
    }
}
