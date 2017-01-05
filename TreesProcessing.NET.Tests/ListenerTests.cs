using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
                    .FirstOrDefault(listenerEvent => listenerEvent.Name == exitEventName) != null,
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

        [Test]
        public void EventListener_Static()
        {
            IEventListener listener = new StaticEventListener();
            List<string> invokeSequence = CreateAndWalkTree(listener);
            CheckInvokeSequence(invokeSequence, false);
        }

        [Test]
        [Ignore("PlatformNotSupportedException for PCL")]
        public void EventListener_Dynamic()
        {
            var listener = new DynamicEventListener();
            List<string> invokeSequence = CreateAndWalkTree(listener);
            CheckInvokeSequence(invokeSequence, true);
        }

        private static List<string> CreateAndWalkTree(IEventListener listener)
        {
            var sampleTree = SampleTree.Init();

            var invokeSequence = new List<string>();
            listener.EnterNode += (s, e) => invokeSequence.Add(Enter + nameof(Node));
            listener.EnterExpression += (s, e) => invokeSequence.Add(Enter + nameof(Expression));
            listener.EnterTerminal += (s, e) => invokeSequence.Add(Enter + nameof(Terminal));
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
            listener.ExitTerminal += (s, e) => invokeSequence.Add(Exit + nameof(Terminal));
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

            listener.Walk(sampleTree);
            return invokeSequence;
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
            var invokeSequence = new List<string>();
            var sampleTree = SampleTree.Init();
            var mock = new Mock<StaticListener>();

            mock.Setup(listener => listener.Enter(It.IsAny<Node>())).Callback((Node s) => invokeSequence.Add(Enter + nameof(Node)));
            mock.Setup(listener => listener.Enter(It.IsAny<Expression>())).Callback((Expression s) => invokeSequence.Add(Enter + nameof(Expression)));
            mock.Setup(listener => listener.Enter(It.IsAny<Terminal>())).Callback((Terminal s) => invokeSequence.Add(Enter + nameof(Terminal)));
            mock.Setup(listener => listener.Enter(It.IsAny<Statement>())).Callback((Statement s) => invokeSequence.Add(Enter + nameof(Statement)));
            mock.Setup(listener => listener.Enter(It.IsAny<BinaryOperatorExpression>())).Callback((BinaryOperatorExpression s) => invokeSequence.Add(Enter + nameof(BinaryOperatorExpression)));
            mock.Setup(listener => listener.Enter(It.IsAny<InvocationExpression>())).Callback((InvocationExpression s) => invokeSequence.Add(Enter + nameof(InvocationExpression)));
            mock.Setup(listener => listener.Enter(It.IsAny<MemberReferenceExpression>())).Callback((MemberReferenceExpression s) => invokeSequence.Add(Enter + nameof(MemberReferenceExpression)));
            mock.Setup(listener => listener.Enter(It.IsAny<UnaryOperatorExpression>())).Callback((UnaryOperatorExpression s) => invokeSequence.Add(Enter + nameof(UnaryOperatorExpression)));
            mock.Setup(listener => listener.Enter(It.IsAny<BooleanLiteral>())).Callback((BooleanLiteral s) => invokeSequence.Add(Enter + nameof(BooleanLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<FloatLiteral>())).Callback((FloatLiteral s) => invokeSequence.Add(Enter + nameof(FloatLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<IntegerLiteral>())).Callback((IntegerLiteral s) => invokeSequence.Add(Enter + nameof(IntegerLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<NullLiteral>())).Callback((NullLiteral s) => invokeSequence.Add(Enter + nameof(NullLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<StringLiteral>())).Callback((StringLiteral s) => invokeSequence.Add(Enter + nameof(StringLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<Identifier>())).Callback((Identifier s) => invokeSequence.Add(Enter + nameof(Identifier)));
            mock.Setup(listener => listener.Enter(It.IsAny<BlockStatement>())).Callback((BlockStatement s) => invokeSequence.Add(Enter + nameof(BlockStatement)));
            mock.Setup(listener => listener.Enter(It.IsAny<ExpressionStatement>())).Callback((ExpressionStatement s) => invokeSequence.Add(Enter + nameof(ExpressionStatement)));
            mock.Setup(listener => listener.Enter(It.IsAny<ForStatement>())).Callback((ForStatement s) => invokeSequence.Add(Enter + nameof(ForStatement)));
            mock.Setup(listener => listener.Enter(It.IsAny<IfElseStatement>())).Callback((IfElseStatement s) => invokeSequence.Add(Enter + nameof(IfElseStatement)));

            mock.Setup(listener => listener.Exit(It.IsAny<Node>())).Callback((Node s) => invokeSequence.Add(Exit + nameof(Node)));
            mock.Setup(listener => listener.Exit(It.IsAny<Expression>())).Callback((Expression s) => invokeSequence.Add(Exit + nameof(Expression)));
            mock.Setup(listener => listener.Exit(It.IsAny<Terminal>())).Callback((Terminal s) => invokeSequence.Add(Exit + nameof(Terminal)));
            mock.Setup(listener => listener.Exit(It.IsAny<Statement>())).Callback((Statement s) => invokeSequence.Add(Exit + nameof(Statement)));
            mock.Setup(listener => listener.Exit(It.IsAny<BinaryOperatorExpression>())).Callback((BinaryOperatorExpression s) => invokeSequence.Add(Exit + nameof(BinaryOperatorExpression)));
            mock.Setup(listener => listener.Exit(It.IsAny<InvocationExpression>())).Callback((InvocationExpression s) => invokeSequence.Add(Exit + nameof(InvocationExpression)));
            mock.Setup(listener => listener.Exit(It.IsAny<MemberReferenceExpression>())).Callback((MemberReferenceExpression s) => invokeSequence.Add(Exit + nameof(MemberReferenceExpression)));
            mock.Setup(listener => listener.Exit(It.IsAny<UnaryOperatorExpression>())).Callback((UnaryOperatorExpression s) => invokeSequence.Add(Exit + nameof(UnaryOperatorExpression)));
            mock.Setup(listener => listener.Exit(It.IsAny<BooleanLiteral>())).Callback((BooleanLiteral s) => invokeSequence.Add(Exit + nameof(BooleanLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<FloatLiteral>())).Callback((FloatLiteral s) => invokeSequence.Add(Exit + nameof(FloatLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<IntegerLiteral>())).Callback((IntegerLiteral s) => invokeSequence.Add(Exit + nameof(IntegerLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<NullLiteral>())).Callback((NullLiteral s) => invokeSequence.Add(Exit + nameof(NullLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<StringLiteral>())).Callback((StringLiteral s) => invokeSequence.Add(Exit + nameof(StringLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<Identifier>())).Callback((Identifier s) => invokeSequence.Add(Exit + nameof(Identifier)));
            mock.Setup(listener => listener.Exit(It.IsAny<BlockStatement>())).Callback((BlockStatement s) => invokeSequence.Add(Exit + nameof(BlockStatement)));
            mock.Setup(listener => listener.Exit(It.IsAny<ExpressionStatement>())).Callback((ExpressionStatement s) => invokeSequence.Add(Exit + nameof(ExpressionStatement)));
            mock.Setup(listener => listener.Exit(It.IsAny<ForStatement>())).Callback((ForStatement s) => invokeSequence.Add(Exit + nameof(ForStatement)));
            mock.Setup(listener => listener.Exit(It.IsAny<IfElseStatement>())).Callback((IfElseStatement s) => invokeSequence.Add(Exit + nameof(IfElseStatement)));

            mock.Object.Walk(sampleTree);

            return invokeSequence;
        }

        private static List<string> GetInvokeSequenceFromDynamicListener()
        {
            var invokeSequence = new List<string>();
            var sampleTree = SampleTree.Init();
            var mock = new Mock<DynamicListener>();
            
            mock.Setup(listener => listener.Enter(It.IsAny<Expression>())).Callback((Expression s) => invokeSequence.Add(Enter + nameof(Expression)));
            mock.Setup(listener => listener.Enter(It.IsAny<Terminal>())).Callback((Terminal s) => invokeSequence.Add(Enter + nameof(Terminal)));
            mock.Setup(listener => listener.Enter(It.IsAny<Statement>())).Callback((Statement s) => invokeSequence.Add(Enter + nameof(Statement)));
            mock.Setup(listener => listener.Enter(It.IsAny<BinaryOperatorExpression>())).Callback((BinaryOperatorExpression s) => invokeSequence.Add(Enter + nameof(BinaryOperatorExpression)));
            mock.Setup(listener => listener.Enter(It.IsAny<InvocationExpression>())).Callback((InvocationExpression s) => invokeSequence.Add(Enter + nameof(InvocationExpression)));
            mock.Setup(listener => listener.Enter(It.IsAny<MemberReferenceExpression>())).Callback((MemberReferenceExpression s) => invokeSequence.Add(Enter + nameof(MemberReferenceExpression)));
            mock.Setup(listener => listener.Enter(It.IsAny<UnaryOperatorExpression>())).Callback((UnaryOperatorExpression s) => invokeSequence.Add(Enter + nameof(UnaryOperatorExpression)));
            mock.Setup(listener => listener.Enter(It.IsAny<BooleanLiteral>())).Callback((BooleanLiteral s) => invokeSequence.Add(Enter + nameof(BooleanLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<FloatLiteral>())).Callback((FloatLiteral s) => invokeSequence.Add(Enter + nameof(FloatLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<IntegerLiteral>())).Callback((IntegerLiteral s) => invokeSequence.Add(Enter + nameof(IntegerLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<NullLiteral>())).Callback((NullLiteral s) => invokeSequence.Add(Enter + nameof(NullLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<StringLiteral>())).Callback((StringLiteral s) => invokeSequence.Add(Enter + nameof(StringLiteral)));
            mock.Setup(listener => listener.Enter(It.IsAny<Identifier>())).Callback((Identifier s) => invokeSequence.Add(Enter + nameof(Identifier)));
            mock.Setup(listener => listener.Enter(It.IsAny<BlockStatement>())).Callback((BlockStatement s) => invokeSequence.Add(Enter + nameof(BlockStatement)));
            mock.Setup(listener => listener.Enter(It.IsAny<ExpressionStatement>())).Callback((ExpressionStatement s) => invokeSequence.Add(Enter + nameof(ExpressionStatement)));
            mock.Setup(listener => listener.Enter(It.IsAny<ForStatement>())).Callback((ForStatement s) => invokeSequence.Add(Enter + nameof(ForStatement)));
            mock.Setup(listener => listener.Enter(It.IsAny<IfElseStatement>())).Callback((IfElseStatement s) => invokeSequence.Add(Enter + nameof(IfElseStatement)));
            
            mock.Setup(listener => listener.Exit(It.IsAny<Expression>())).Callback((Expression s) => invokeSequence.Add(Exit + nameof(Expression)));
            mock.Setup(listener => listener.Exit(It.IsAny<Terminal>())).Callback((Terminal s) => invokeSequence.Add(Exit + nameof(Terminal)));
            mock.Setup(listener => listener.Exit(It.IsAny<Statement>())).Callback((Statement s) => invokeSequence.Add(Exit + nameof(Statement)));
            mock.Setup(listener => listener.Exit(It.IsAny<BinaryOperatorExpression>())).Callback((BinaryOperatorExpression s) => invokeSequence.Add(Exit + nameof(BinaryOperatorExpression)));
            mock.Setup(listener => listener.Exit(It.IsAny<InvocationExpression>())).Callback((InvocationExpression s) => invokeSequence.Add(Exit + nameof(InvocationExpression)));
            mock.Setup(listener => listener.Exit(It.IsAny<MemberReferenceExpression>())).Callback((MemberReferenceExpression s) => invokeSequence.Add(Exit + nameof(MemberReferenceExpression)));
            mock.Setup(listener => listener.Exit(It.IsAny<UnaryOperatorExpression>())).Callback((UnaryOperatorExpression s) => invokeSequence.Add(Exit + nameof(UnaryOperatorExpression)));
            mock.Setup(listener => listener.Exit(It.IsAny<BooleanLiteral>())).Callback((BooleanLiteral s) => invokeSequence.Add(Exit + nameof(BooleanLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<FloatLiteral>())).Callback((FloatLiteral s) => invokeSequence.Add(Exit + nameof(FloatLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<IntegerLiteral>())).Callback((IntegerLiteral s) => invokeSequence.Add(Exit + nameof(IntegerLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<NullLiteral>())).Callback((NullLiteral s) => invokeSequence.Add(Exit + nameof(NullLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<StringLiteral>())).Callback((StringLiteral s) => invokeSequence.Add(Exit + nameof(StringLiteral)));
            mock.Setup(listener => listener.Exit(It.IsAny<Identifier>())).Callback((Identifier s) => invokeSequence.Add(Exit + nameof(Identifier)));
            mock.Setup(listener => listener.Exit(It.IsAny<BlockStatement>())).Callback((BlockStatement s) => invokeSequence.Add(Exit + nameof(BlockStatement)));
            mock.Setup(listener => listener.Exit(It.IsAny<ExpressionStatement>())).Callback((ExpressionStatement s) => invokeSequence.Add(Exit + nameof(ExpressionStatement)));
            mock.Setup(listener => listener.Exit(It.IsAny<ForStatement>())).Callback((ForStatement s) => invokeSequence.Add(Exit + nameof(ForStatement)));
            mock.Setup(listener => listener.Exit(It.IsAny<IfElseStatement>())).Callback((IfElseStatement s) => invokeSequence.Add(Exit + nameof(IfElseStatement)));

            mock.Object.Walk(sampleTree);

            return invokeSequence;
        }
    }
}
