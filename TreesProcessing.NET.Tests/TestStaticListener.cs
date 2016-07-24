using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET.Tests
{
    public class TestStaticListener : StaticListener
    {
        private List<string> _callSequence = new List<string>();
        public IReadOnlyList<string> CallSequence => _callSequence;

        public override void Enter(Terminal terminal)
        {
            
        }

        public override void Enter(Statement statement)
        {
        }

        public override void Enter(Expression expression)
        {
        }

        public override void Exit(Expression exrpession)
        {
        }

        public override void Enter(BinaryOperatorExpression binaryOperatorExpression)
        {
        }

        public override void Enter(MemberReferenceExpression memberReferenceExpression)
        {
        }

        public override void Enter(UnaryOperatorExpression unaryOperatorExpression)
        {
        }

        public override void Enter(InvocationExpression invocationExpression)
        {
        }

        public override void Enter(BooleanLiteral unaryOperatorExpression)
        {
        }

        public override void Enter(IntegerLiteral integerLiteral)
        {
        }

        public override void Enter(StringLiteral stringLiteral)
        {
        }

        public override void Enter(BlockStatement blockStatement)
        {
        }

        public override void Enter(ForStatement forStatement)
        {
        }

        public override void Enter(IfElseStatement ifElseStatement)
        {
        }

        public override void Enter(ExpressionStatement expressionStatement)
        {
        }

        public override void Enter(Identifier identifier)
        {
        }

        public override void Enter(NullLiteral nullLiteral)
        {
        }

        public override void Enter(FloatLiteral floatLiteral)
        {
        }

        public override void Enter(Node node)
        {
        }

        public override void Exit(BlockStatement blockStatement)
        {
        }

        public override void Exit(ExpressionStatement expressionStatement)
        {
        }

        public override void Exit(ForStatement forStatement)
        {
        }

        public override void Exit(IfElseStatement ifElseStatement)
        {
        }

        public override void Exit(BinaryOperatorExpression binaryOperatorExpression)
        {
        }

        public override void Exit(MemberReferenceExpression memberReferenceExpression)
        {
        }

        public override void Exit(UnaryOperatorExpression unaryOperatorExpression)
        {
        }

        public override void Exit(InvocationExpression invocationExpression)
        {
        }

        public override void Exit(Statement statement)
        {
        }

        public override void Exit(Terminal terminal)
        {
        }

        public override void Exit(Node node)
        {
        }

        public override void Exit(BooleanLiteral unaryOperatorExpression)
        {
        }

        public override void Exit(FloatLiteral floatLiteral)
        {
        }

        public override void Exit(IntegerLiteral integerLiteral)
        {
        }

        public override void Exit(NullLiteral nullLiteral)
        {
        }

        public override void Exit(StringLiteral stringLiteral)
        {
        }

        public override void Exit(Identifier identifier)
        {
        }
    }
}
