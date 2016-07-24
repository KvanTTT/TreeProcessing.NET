using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET.Tests
{
    public class ListenerTests
    {
        private const string Enter = "Enter";
        private const string Exit = "Exit";

        private static string[] BeginExpectedSequence = new string[]
        {
            Enter + nameof(Node), Enter + nameof(Statement), Enter + nameof(BlockStatement),
                Enter + nameof(Statement), Enter + nameof(ExpressionStatement),
                    Enter + nameof(Expression), Enter + nameof(BinaryOperatorExpression),
                        Enter + nameof(Expression), Enter + nameof(Terminal), Enter + nameof(Identifier),
                        Exit + nameof(Identifier), Exit + nameof(Terminal), Exit + nameof(Expression),
                        Enter + nameof(Expression), Enter + nameof(Terminal), Enter + nameof(IntegerLiteral),
                        Exit + nameof(IntegerLiteral), Exit + nameof(Terminal), Exit + nameof(Expression),
                    Exit + nameof(BinaryOperatorExpression), Exit + nameof(Expression),
                Exit + nameof(ExpressionStatement), Exit + nameof(Statement)
        };

        private static string[] EndExpectedSequence = new string[]
        {
                                    Enter + nameof(Expression), Enter + nameof(Terminal), Enter + nameof(Identifier),
                                Exit + nameof(Identifier), Exit + nameof(Terminal), Exit + nameof(Expression),
                            Exit + nameof(InvocationExpression), Exit + nameof(Expression),
                        Exit + nameof(ExpressionStatement), Exit + nameof(Statement),
                    Exit + nameof(IfElseStatement), Exit + nameof(Statement),
                Exit + nameof(ForStatement), Exit + nameof(Statement),
            Exit + nameof(BlockStatement), Exit + nameof(Statement), Exit + nameof(Node)
        };

        [Test]
        public void CheckAllListenerMethodsExists()
        {
            MethodInfo[] listenerMethods = typeof(IListener).GetMethods();
            IEnumerable<Type> nodeTypes = Assembly.GetAssembly(typeof(Node)).GetTypes()
                .Where(t => t == typeof(Node) || t.IsSubclassOf(typeof(Node)));
            foreach (Type type in nodeTypes)
            {
                Assert.IsTrue(listenerMethods
                    .FirstOrDefault(methodInfo =>
                    {
                        var parameters = methodInfo.GetParameters();
                        return methodInfo.Name == "Enter" && parameters.Length > 0 && parameters[0].ParameterType == type;
                    }) != null,
                    $"Enter method for Type {type} is not exists");

                Assert.IsTrue(listenerMethods
                    .FirstOrDefault(methodInfo =>
                    {
                        var parameters = methodInfo.GetParameters();
                        return methodInfo.Name == "Exit" && parameters.Length > 0 && parameters[0].ParameterType == type;
                    }) != null,
                    $"Visitor for Type {type} is not exists");
            }
        }

        [Test]
        public void CheckAllEventListenerMethodsExists()
        {
            EventInfo[] listenerEvents = typeof(IEventListener).GetEvents();
            IEnumerable<Type> nodeTypes = Assembly.GetAssembly(typeof(Node)).GetTypes()
                .Where(t => t == typeof(Node) || t.IsSubclassOf(typeof(Node)));
            foreach (Type type in nodeTypes)
            {
                var enterEventName = "Enter" + type.Name;
                var exitEventName = "Exit" + type.Name;
                Assert.IsTrue(listenerEvents
                    .FirstOrDefault(listenerEvent =>
                    {
                        return listenerEvent.Name == enterEventName;
                    }) != null,
                    $"Enter method for Type {type} is not exists");

                Assert.IsTrue(listenerEvents
                    .FirstOrDefault(listenerEvent =>
                    {;
                        return listenerEvent.Name == exitEventName;
                    }) != null,
                    $"Visitor for Type {type} is not exists");
            }
        }

        [Test]
        public void Listener_Static()
        {
            var invokeSequence = GetInvokeSequenceFromStaticListener();
            CheckInvokeSequence(invokeSequence, false);
        }

        [Test]
        public void Listener_Dynamic()
        {
            var invokeSequence = GetInvokeSequenceFromDynamicListener();
            CheckInvokeSequence(invokeSequence, true);
        }

        private static void CheckInvokeSequence(List<string> invokeSequence, bool excludeAbstract)
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
            CollectionAssert.AreEqual(beginExpectedSequence, actualSequence);

            actualSequence = invokeSequence.Skip(invokeSequence.Count() - endExpectedSequence.Length);
            CollectionAssert.AreEqual(endExpectedSequence, actualSequence);
        }

        private static List<string> GetInvokeSequenceFromStaticListener()
        {
            var invokesSequence = new List<string>();
            var sampleTree = SampleTree.Init();
            var mock = new Mock<StaticListener>();

            mock.Setup(listener => listener.Enter(It.IsAny<Node>())).Callback((Node s) => invokesSequence.Add(Enter + nameof(Node)));
            mock.Setup(listener => listener.Enter(It.IsAny<Expression>())).Callback((Expression s) => invokesSequence.Add(Enter + nameof(Expression)));
            mock.Setup(listener => listener.Enter(It.IsAny<Terminal>())).Callback((Terminal s) => invokesSequence.Add(Enter + nameof(Terminal)));
            mock.Setup(listener => listener.Enter(It.IsAny<Statement>())).Callback((Statement s) => invokesSequence.Add(Enter + nameof(Statement)));
            mock.Setup(listener => listener.Enter(It.IsAny<BinaryOperatorExpression>())).Callback((BinaryOperatorExpression s) => invokesSequence.Add(Enter + nameof(BinaryOperatorExpression)));
            mock.Setup(listener => listener.Enter(It.IsAny<InvocationExpression>())).Callback((InvocationExpression s) => invokesSequence.Add(Enter + nameof(InvocationExpression)));
            mock.Setup(listener => listener.Enter(It.IsAny<MemberReferenceExpression>())).Callback((MemberReferenceExpression s) => invokesSequence.Add(Enter + nameof(MemberReferenceExpression)));
            mock.Setup(listener => listener.Enter(It.IsAny<UnaryOperatorExpression>())).Callback((UnaryOperatorExpression s) => invokesSequence.Add(Enter + nameof(UnaryOperatorExpression)));
            mock.Setup(listener => listener.Enter(It.IsAny<BooleanLiteral>())).Callback((BooleanLiteral s) => invokesSequence.Add(Enter + nameof(BooleanLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<FloatLiteral>())).Callback((FloatLiteral s) => invokesSequence.Add(Enter + nameof(FloatLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<IntegerLiteral>())).Callback((IntegerLiteral s) => invokesSequence.Add(Enter + nameof(IntegerLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<NullLiteral>())).Callback((NullLiteral s) => invokesSequence.Add(Enter + nameof(NullLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<StringLiteral>())).Callback((StringLiteral s) => invokesSequence.Add(Enter + nameof(StringLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<Identifier>())).Callback((Identifier s) => invokesSequence.Add(Enter + nameof(Identifier)));
            mock.Setup(listener => listener.Enter(It.IsAny<BlockStatement>())).Callback((BlockStatement s) => invokesSequence.Add(Enter + nameof(BlockStatement)));
            mock.Setup(listener => listener.Enter(It.IsAny<ExpressionStatement>())).Callback((ExpressionStatement s) => invokesSequence.Add(Enter + nameof(ExpressionStatement)));
            mock.Setup(listener => listener.Enter(It.IsAny<ForStatement>())).Callback((ForStatement s) => invokesSequence.Add(Enter + nameof(ForStatement)));
            mock.Setup(listener => listener.Enter(It.IsAny<IfElseStatement>())).Callback((IfElseStatement s) => invokesSequence.Add(Enter + nameof(IfElseStatement)));

            mock.Setup(listener => listener.Exit(It.IsAny<Node>())).Callback((Node s) => invokesSequence.Add(Exit + nameof(Node)));
            mock.Setup(listener => listener.Exit(It.IsAny<Expression>())).Callback((Expression s) => invokesSequence.Add(Exit + nameof(Expression)));
            mock.Setup(listener => listener.Exit(It.IsAny<Terminal>())).Callback((Terminal s) => invokesSequence.Add(Exit + nameof(Terminal)));
            mock.Setup(listener => listener.Exit(It.IsAny<Statement>())).Callback((Statement s) => invokesSequence.Add(Exit + nameof(Statement)));
            mock.Setup(listener => listener.Exit(It.IsAny<BinaryOperatorExpression>())).Callback((BinaryOperatorExpression s) => invokesSequence.Add(Exit + nameof(BinaryOperatorExpression)));
            mock.Setup(listener => listener.Exit(It.IsAny<InvocationExpression>())).Callback((InvocationExpression s) => invokesSequence.Add(Exit + nameof(InvocationExpression)));
            mock.Setup(listener => listener.Exit(It.IsAny<MemberReferenceExpression>())).Callback((MemberReferenceExpression s) => invokesSequence.Add(Exit + nameof(MemberReferenceExpression)));
            mock.Setup(listener => listener.Exit(It.IsAny<UnaryOperatorExpression>())).Callback((UnaryOperatorExpression s) => invokesSequence.Add(Exit + nameof(UnaryOperatorExpression)));
            mock.Setup(listener => listener.Exit(It.IsAny<BooleanLiteral>())).Callback((BooleanLiteral s) => invokesSequence.Add(Exit + nameof(BooleanLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<FloatLiteral>())).Callback((FloatLiteral s) => invokesSequence.Add(Exit + nameof(FloatLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<IntegerLiteral>())).Callback((IntegerLiteral s) => invokesSequence.Add(Exit + nameof(IntegerLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<NullLiteral>())).Callback((NullLiteral s) => invokesSequence.Add(Exit + nameof(NullLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<StringLiteral>())).Callback((StringLiteral s) => invokesSequence.Add(Exit + nameof(StringLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<Identifier>())).Callback((Identifier s) => invokesSequence.Add(Exit + nameof(Identifier)));
            mock.Setup(listener => listener.Exit(It.IsAny<BlockStatement>())).Callback((BlockStatement s) => invokesSequence.Add(Exit + nameof(BlockStatement)));
            mock.Setup(listener => listener.Exit(It.IsAny<ExpressionStatement>())).Callback((ExpressionStatement s) => invokesSequence.Add(Exit + nameof(ExpressionStatement)));
            mock.Setup(listener => listener.Exit(It.IsAny<ForStatement>())).Callback((ForStatement s) => invokesSequence.Add(Exit + nameof(ForStatement)));
            mock.Setup(listener => listener.Exit(It.IsAny<IfElseStatement>())).Callback((IfElseStatement s) => invokesSequence.Add(Exit + nameof(IfElseStatement)));

            mock.Object.Walk(sampleTree);

            return invokesSequence;
        }

        private static List<string> GetInvokeSequenceFromDynamicListener()
        {
            var invokesSequence = new List<string>();
            var sampleTree = SampleTree.Init();
            var mock = new Mock<DynamicListener>();

            mock.Setup(listener => listener.Enter(It.IsAny<Node>())).Callback((Node s) => invokesSequence.Add(Enter + nameof(Node)));
            mock.Setup(listener => listener.Enter(It.IsAny<Expression>())).Callback((Expression s) => invokesSequence.Add(Enter + nameof(Expression)));
            mock.Setup(listener => listener.Enter(It.IsAny<Terminal>())).Callback((Terminal s) => invokesSequence.Add(Enter + nameof(Terminal)));
            mock.Setup(listener => listener.Enter(It.IsAny<Statement>())).Callback((Statement s) => invokesSequence.Add(Enter + nameof(Statement)));
            mock.Setup(listener => listener.Enter(It.IsAny<BinaryOperatorExpression>())).Callback((BinaryOperatorExpression s) => invokesSequence.Add(Enter + nameof(BinaryOperatorExpression)));
            mock.Setup(listener => listener.Enter(It.IsAny<InvocationExpression>())).Callback((InvocationExpression s) => invokesSequence.Add(Enter + nameof(InvocationExpression)));
            mock.Setup(listener => listener.Enter(It.IsAny<MemberReferenceExpression>())).Callback((MemberReferenceExpression s) => invokesSequence.Add(Enter + nameof(MemberReferenceExpression)));
            mock.Setup(listener => listener.Enter(It.IsAny<UnaryOperatorExpression>())).Callback((UnaryOperatorExpression s) => invokesSequence.Add(Enter + nameof(UnaryOperatorExpression)));
            mock.Setup(listener => listener.Enter(It.IsAny<BooleanLiteral>())).Callback((BooleanLiteral s) => invokesSequence.Add(Enter + nameof(BooleanLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<FloatLiteral>())).Callback((FloatLiteral s) => invokesSequence.Add(Enter + nameof(FloatLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<IntegerLiteral>())).Callback((IntegerLiteral s) => invokesSequence.Add(Enter + nameof(IntegerLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<NullLiteral>())).Callback((NullLiteral s) => invokesSequence.Add(Enter + nameof(NullLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<StringLiteral>())).Callback((StringLiteral s) => invokesSequence.Add(Enter + nameof(StringLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<Identifier>())).Callback((Identifier s) => invokesSequence.Add(Enter + nameof(Identifier)));
            mock.Setup(listener => listener.Enter(It.IsAny<BlockStatement>())).Callback((BlockStatement s) => invokesSequence.Add(Enter + nameof(BlockStatement)));
            mock.Setup(listener => listener.Enter(It.IsAny<ExpressionStatement>())).Callback((ExpressionStatement s) => invokesSequence.Add(Enter + nameof(ExpressionStatement)));
            mock.Setup(listener => listener.Enter(It.IsAny<ForStatement>())).Callback((ForStatement s) => invokesSequence.Add(Enter + nameof(ForStatement)));
            mock.Setup(listener => listener.Enter(It.IsAny<IfElseStatement>())).Callback((IfElseStatement s) => invokesSequence.Add(Enter + nameof(IfElseStatement)));

            mock.Setup(listener => listener.Exit(It.IsAny<Node>())).Callback((Node s) => invokesSequence.Add(Exit + nameof(Node)));
            mock.Setup(listener => listener.Exit(It.IsAny<Expression>())).Callback((Expression s) => invokesSequence.Add(Exit + nameof(Expression)));
            mock.Setup(listener => listener.Exit(It.IsAny<Terminal>())).Callback((Terminal s) => invokesSequence.Add(Exit + nameof(Terminal)));
            mock.Setup(listener => listener.Exit(It.IsAny<Statement>())).Callback((Statement s) => invokesSequence.Add(Exit + nameof(Statement)));
            mock.Setup(listener => listener.Exit(It.IsAny<BinaryOperatorExpression>())).Callback((BinaryOperatorExpression s) => invokesSequence.Add(Exit + nameof(BinaryOperatorExpression)));
            mock.Setup(listener => listener.Exit(It.IsAny<InvocationExpression>())).Callback((InvocationExpression s) => invokesSequence.Add(Exit + nameof(InvocationExpression)));
            mock.Setup(listener => listener.Exit(It.IsAny<MemberReferenceExpression>())).Callback((MemberReferenceExpression s) => invokesSequence.Add(Exit + nameof(MemberReferenceExpression)));
            mock.Setup(listener => listener.Exit(It.IsAny<UnaryOperatorExpression>())).Callback((UnaryOperatorExpression s) => invokesSequence.Add(Exit + nameof(UnaryOperatorExpression)));
            mock.Setup(listener => listener.Exit(It.IsAny<BooleanLiteral>())).Callback((BooleanLiteral s) => invokesSequence.Add(Exit + nameof(BooleanLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<FloatLiteral>())).Callback((FloatLiteral s) => invokesSequence.Add(Exit + nameof(FloatLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<IntegerLiteral>())).Callback((IntegerLiteral s) => invokesSequence.Add(Exit + nameof(IntegerLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<NullLiteral>())).Callback((NullLiteral s) => invokesSequence.Add(Exit + nameof(NullLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<StringLiteral>())).Callback((StringLiteral s) => invokesSequence.Add(Exit + nameof(StringLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<Identifier>())).Callback((Identifier s) => invokesSequence.Add(Exit + nameof(Identifier)));
            mock.Setup(listener => listener.Exit(It.IsAny<BlockStatement>())).Callback((BlockStatement s) => invokesSequence.Add(Exit + nameof(BlockStatement)));
            mock.Setup(listener => listener.Exit(It.IsAny<ExpressionStatement>())).Callback((ExpressionStatement s) => invokesSequence.Add(Exit + nameof(ExpressionStatement)));
            mock.Setup(listener => listener.Exit(It.IsAny<ForStatement>())).Callback((ForStatement s) => invokesSequence.Add(Exit + nameof(ForStatement)));
            mock.Setup(listener => listener.Exit(It.IsAny<IfElseStatement>())).Callback((IfElseStatement s) => invokesSequence.Add(Exit + nameof(IfElseStatement)));

            mock.Object.Walk(sampleTree);

            return invokesSequence;
        }
    }
}
