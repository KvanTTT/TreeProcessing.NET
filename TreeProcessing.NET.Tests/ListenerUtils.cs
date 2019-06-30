using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace TreeProcessing.NET.Tests
{
    public class ListenerUtils
    {
        private const string Enter = "Enter";
        private const string Exit = "Exit";

        private static string[] BeginExpectedSequence = new[]
        {
            Enter + nameof(Node), Enter + nameof(Statement), Enter + nameof(BlockStatement),
                Enter + nameof(Statement), Enter + nameof(ExpressionStatement),
                    Enter + nameof(Expression), Enter + nameof(BinaryOperatorExpression),
                        Enter + nameof(Expression), Enter + nameof(Token), Enter + nameof(Identifier),
                        Exit + nameof(Identifier), Exit + nameof(Token), Exit + nameof(Expression),
                        Enter + nameof(Expression), Enter + nameof(Token), Enter + nameof(IntegerLiteral),
                        Exit + nameof(IntegerLiteral), Exit + nameof(Token), Exit + nameof(Expression),
                    Exit + nameof(BinaryOperatorExpression), Exit + nameof(Expression),
                Exit + nameof(ExpressionStatement), Exit + nameof(Statement)
        };

        private static string[] EndExpectedSequence = new[]
        {
                                    Enter + nameof(Expression), Enter + nameof(Token), Enter + nameof(Identifier),
                                Exit + nameof(Identifier), Exit + nameof(Token), Exit + nameof(Expression),
                            Exit + nameof(InvocationExpression), Exit + nameof(Expression),
                        Exit + nameof(ExpressionStatement), Exit + nameof(Statement),
                    Exit + nameof(IfElseStatement), Exit + nameof(Statement),
                Exit + nameof(ForStatement), Exit + nameof(Statement),
            Exit + nameof(BlockStatement), Exit + nameof(Statement), Exit + nameof(Node)
        };

        public static List<string> AppendEvents(IEventListener listener)
        {
            var invokeSequence = new List<string>();

            listener.EnterNode += (s, e) => invokeSequence.Add(Enter + nameof(Node));
            listener.EnterExpression += (s, e) => invokeSequence.Add(Enter + nameof(Expression));
            listener.EnterToken += (s, e) => invokeSequence.Add(Enter + nameof(Token));
            listener.EnterStatement += (s, e) => invokeSequence.Add(Enter + nameof(Statement));
            listener.EnterBinaryOperatorExpression += (s, e) => invokeSequence.Add(Enter + nameof(BinaryOperatorExpression));
            listener.EnterInvocationExpression += (s, e) => invokeSequence.Add(Enter + nameof(InvocationExpression));
            listener.EnterMemberReferenceExpression += (s, e) => invokeSequence.Add(Enter + nameof(MemberReferenceExpression));
            listener.EnterUnaryOperatorExpression += (s, e) => invokeSequence.Add(Enter + nameof(UnaryOperatorExpression));
            listener.EnterBooleanLiteral += (s, e) => invokeSequence.Add(Enter + nameof(BooleanLiteral));
            listener.EnterFloatLiteral += (s, e) => invokeSequence.Add(Enter + nameof(FloatLiteral));
            listener.EnterIntegerLiteral += (s, e) => invokeSequence.Add(Enter + nameof(IntegerLiteral));
            listener.EnterNullLiteral += (s, e) => invokeSequence.Add(Enter + nameof(NullLiteral));
            listener.EnterStringLiteral += (s, e) => invokeSequence.Add(Enter + nameof(StringLiteral));
            listener.EnterIdentifier += (s, e) => invokeSequence.Add(Enter + nameof(Identifier));
            listener.EnterBlockStatement += (s, e) => invokeSequence.Add(Enter + nameof(BlockStatement));
            listener.EnterExpressionStatement += (s, e) => invokeSequence.Add(Enter + nameof(ExpressionStatement));
            listener.EnterForStatement += (s, e) => invokeSequence.Add(Enter + nameof(ForStatement));
            listener.EnterIfElseStatement += (s, e) => invokeSequence.Add(Enter + nameof(IfElseStatement));

            listener.ExitNode += (s, e) => invokeSequence.Add(Exit + nameof(Node));
            listener.ExitExpression += (s, e) => invokeSequence.Add(Exit + nameof(Expression));
            listener.ExitToken += (s, e) => invokeSequence.Add(Exit + nameof(Token));
            listener.ExitStatement += (s, e) => invokeSequence.Add(Exit + nameof(Statement));
            listener.ExitBinaryOperatorExpression += (s, e) => invokeSequence.Add(Exit + nameof(BinaryOperatorExpression));
            listener.ExitInvocationExpression += (s, e) => invokeSequence.Add(Exit + nameof(InvocationExpression));
            listener.ExitMemberReferenceExpression += (s, e) => invokeSequence.Add(Exit + nameof(MemberReferenceExpression));
            listener.ExitUnaryOperatorExpression += (s, e) => invokeSequence.Add(Exit + nameof(UnaryOperatorExpression));
            listener.ExitBooleanLiteral += (s, e) => invokeSequence.Add(Exit + nameof(BooleanLiteral));
            listener.ExitFloatLiteral += (s, e) => invokeSequence.Add(Exit + nameof(FloatLiteral));
            listener.ExitIntegerLiteral += (s, e) => invokeSequence.Add(Exit + nameof(IntegerLiteral));
            listener.ExitNullLiteral += (s, e) => invokeSequence.Add(Exit + nameof(NullLiteral));
            listener.ExitStringLiteral += (s, e) => invokeSequence.Add(Exit + nameof(StringLiteral));
            listener.ExitIdentifier += (s, e) => invokeSequence.Add(Exit + nameof(Identifier));
            listener.ExitBlockStatement += (s, e) => invokeSequence.Add(Exit + nameof(BlockStatement));
            listener.ExitExpressionStatement += (s, e) => invokeSequence.Add(Exit + nameof(ExpressionStatement));
            listener.ExitForStatement += (s, e) => invokeSequence.Add(Exit + nameof(ForStatement));
            listener.ExitIfElseStatement += (s, e) => invokeSequence.Add(Exit + nameof(IfElseStatement));

            return invokeSequence;
        }

        public static void CheckInvokeSequence(List<string> invokeSequence, bool excludeAbstract)
        {
            string[] beginExpectedSequence, endExpectedSequence;
            if (excludeAbstract)
            {
                var abstractClasses = Assembly.GetAssembly(typeof(Node)).GetTypes()
                    .Where(t => (t == typeof(Node) || t.IsSubclassOf(typeof(Node))) && t.IsAbstract)
                    .Select(t => t.Name);

                var abstractMethods = abstractClasses.Select(className => Enter + className).ToList();
                abstractMethods.AddRange(abstractClasses.Select(className => Exit + className));

                beginExpectedSequence = BeginExpectedSequence.Where(
                    methodName => !abstractMethods.Contains(methodName)).ToArray();

                endExpectedSequence = EndExpectedSequence.Where(
                    methodName => !abstractMethods.Contains(methodName)).ToArray();
            }
            else
            {
                beginExpectedSequence = BeginExpectedSequence;
                endExpectedSequence = EndExpectedSequence;
            }

            var actualSequence = invokeSequence.Take(beginExpectedSequence.Length);
            Assert.Equal(beginExpectedSequence, actualSequence);

            actualSequence = invokeSequence.Skip(invokeSequence.Count() - endExpectedSequence.Length);
            Assert.Equal(endExpectedSequence, actualSequence);
        }
    }
}
