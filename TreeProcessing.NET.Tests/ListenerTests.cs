using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace TreeProcessing.NET.Tests
{
    public class ListenerTests
    {
        private const string Enter = "Enter";
        private const string Exit = "Exit";

        [Fact]
        public void CheckAllListenerMethodsExists()
        {
            MethodInfo[] listenerMethods = typeof(IListener).GetMethods();
            IEnumerable<Type> nodeTypes = Assembly.GetAssembly(typeof(Node)).GetTypes()
                .Where(t => t == typeof(Node) || t.IsSubclassOf(typeof(Node)));
            foreach (Type type in nodeTypes)
            {
                Assert.True(listenerMethods
                    .FirstOrDefault(methodInfo =>
                    {
                        var parameters = methodInfo.GetParameters();
                        return methodInfo.Name == "Enter" && parameters.Length > 0 && parameters[0].ParameterType == type;
                    }) != null,
                    $"Enter method for Type {type} is not exists");

                Assert.True(listenerMethods
                    .FirstOrDefault(methodInfo =>
                    {
                        var parameters = methodInfo.GetParameters();
                        return methodInfo.Name == "Exit" && parameters.Length > 0 && parameters[0].ParameterType == type;
                    }) != null,
                    $"Visitor for Type {type} is not exists");
            }
        }

        [Fact]
        public void CheckAllEventListenerMethodsExists()
        {
            EventInfo[] listenerEvents = typeof(IEventListener).GetEvents();
            IEnumerable<Type> nodeTypes = Assembly.GetAssembly(typeof(Node)).GetTypes()
                .Where(t => t == typeof(Node) || t.IsSubclassOf(typeof(Node)));
            foreach (Type type in nodeTypes)
            {
                var enterEventName = "Enter" + type.Name;
                var exitEventName = "Exit" + type.Name;
                Assert.True(listenerEvents
                    .FirstOrDefault(listenerEvent =>
                    {
                        return listenerEvent.Name == enterEventName;
                    }) != null,
                    $"Enter method for Type {type} is not exists");

                Assert.True(listenerEvents
                    .FirstOrDefault(listenerEvent => listenerEvent.Name == exitEventName) != null,
                    $"Visitor for Type {type} is not exists");
            }
        }

        [Fact]
        public void Listener_Static()
        {
            var invokeSequence = GetInvokeSequenceFromStaticListener();
            ListenerUtils.CheckInvokeSequence(invokeSequence, false);
        }

        [Fact]
        public void Listener_Dynamic()
        {
            var invokeSequence = GetInvokeSequenceFromDynamicListener();
            ListenerUtils.CheckInvokeSequence(invokeSequence, true);
        }

        [Fact]
        public void EventListener_Static()
        {
            IEventListener listener = new StaticEventListener();
            List<string> invokeSequence = ListenerUtils.AppendEvents(listener);
            listener.Walk(SampleTree.Init());
            ListenerUtils.CheckInvokeSequence(invokeSequence, false);
        }

#if NET
        [Fact]
        public void EventListener_Dynamic()
        {
            var listener = new DynamicEventListener();
            List<string> invokeSequence = ListenerUtils.AppendEvents(listener);
            listener.InitializeEvents();
            listener.Walk(SampleTree.Init());
            ListenerUtils.CheckInvokeSequence(invokeSequence, true);
        }
#endif

        private static List<string> GetInvokeSequenceFromStaticListener()
        {
            var invokeSequence = new List<string>();
            var sampleTree = SampleTree.Init();
            var mock = new Mock<StaticListener>();

            mock.Setup(listener => listener.Enter(It.IsAny<Node>())).Callback((Node s) => invokeSequence.Add(Enter + nameof(Node)));
            mock.Setup(listener => listener.Enter(It.IsAny<Expression>())).Callback((Expression s) => invokeSequence.Add(Enter + nameof(Expression)));
            mock.Setup(listener => listener.Enter(It.IsAny<Token>())).Callback((Token s) => invokeSequence.Add(Enter + nameof(Token)));
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
            mock.Setup(listener => listener.Exit(It.IsAny<Token>())).Callback((Token s) => invokeSequence.Add(Exit + nameof(Token)));
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
            mock.Setup(listener => listener.Enter(It.IsAny<Token>())).Callback((Token s) => invokeSequence.Add(Enter + nameof(Token)));
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
            mock.Setup(listener => listener.Exit(It.IsAny<Token>())).Callback((Token s) => invokeSequence.Add(Exit + nameof(Token)));
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
