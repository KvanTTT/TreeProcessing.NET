using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#pragma warning disable CS0067

namespace TreeProcessing.NET
{
    public class DynamicEventListener : IEventListener
    {
        private Dictionary<Type, Delegate> enterEvents;
        private Dictionary<Type, Delegate> exitEvents;

        public event EventHandler<BinaryOperatorExpression> EnterBinaryOperatorExpression;
        public event EventHandler<BlockStatement> EnterBlockStatement;
        public event EventHandler<BooleanLiteral> EnterBooleanLiteral;
        public event EventHandler<Expression> EnterExpression;
        public event EventHandler<ExpressionStatement> EnterExpressionStatement;
        public event EventHandler<FloatLiteral> EnterFloatLiteral;
        public event EventHandler<ForStatement> EnterForStatement;
        public event EventHandler<Identifier> EnterIdentifier;
        public event EventHandler<IfElseStatement> EnterIfElseStatement;
        public event EventHandler<IntegerLiteral> EnterIntegerLiteral;
        public event EventHandler<InvocationExpression> EnterInvocationExpression;
        public event EventHandler<MemberReferenceExpression> EnterMemberReferenceExpression;
        public event EventHandler<Node> EnterNode;
        public event EventHandler<NullLiteral> EnterNullLiteral;
        public event EventHandler<Statement> EnterStatement;
        public event EventHandler<StringLiteral> EnterStringLiteral;
        public event EventHandler<Terminal> EnterTerminal;
        public event EventHandler<UnaryOperatorExpression> EnterUnaryOperatorExpression;
        public event EventHandler<BinaryOperatorExpression> ExitBinaryOperatorExpression;
        public event EventHandler<BlockStatement> ExitBlockStatement;
        public event EventHandler<BooleanLiteral> ExitBooleanLiteral;
        public event EventHandler<Expression> ExitExpression;
        public event EventHandler<ExpressionStatement> ExitExpressionStatement;
        public event EventHandler<FloatLiteral> ExitFloatLiteral;
        public event EventHandler<ForStatement> ExitForStatement;
        public event EventHandler<Identifier> ExitIdentifier;
        public event EventHandler<IfElseStatement> ExitIfElseStatement;
        public event EventHandler<IntegerLiteral> ExitIntegerLiteral;
        public event EventHandler<InvocationExpression> ExitInvocationExpression;
        public event EventHandler<MemberReferenceExpression> ExitMemberReferenceExpression;
        public event EventHandler<Node> ExitNode;
        public event EventHandler<NullLiteral> ExitNullLiteral;
        public event EventHandler<Statement> ExitStatement;
        public event EventHandler<StringLiteral> ExitStringLiteral;
        public event EventHandler<Terminal> ExitTerminal;
        public event EventHandler<UnaryOperatorExpression> ExitUnaryOperatorExpression;

        public DynamicEventListener()
        {
        }

        public void InitializeEvents()
        {
            var dynamicEventListenerType = typeof(DynamicEventListener);
            IEnumerable<EventInfo> events = dynamicEventListenerType.GetRuntimeEvents();

            enterEvents = new Dictionary<Type, Delegate>();
            exitEvents = new Dictionary<Type, Delegate>();
            foreach (var ev in events)
            {
                var type = ev.EventHandlerType.GenericTypeArguments.First();

                var d = (Delegate)dynamicEventListenerType
                    .GetField(ev.Name, BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(this);
                if (ev.Name.StartsWith("Enter"))
                {
                    enterEvents[type] = d;
                }
                else if (ev.Name.StartsWith("Exit"))
                {
                    exitEvents[type] = d;
                }
            }
        }

        public void Walk(Node node)
        {
            InvokeEnterEvent(node);
            Visit(node);
            InvokeExitEvent(node);
        }

        private void Visit(Node node)
        {
            if (node == null)
            {
                return;
            }

            Type type = node.GetType();
            PropertyInfo[] properties = ReflectionCache.GetClassProperties(type);
            foreach (PropertyInfo prop in properties)
            {
                Type propType = prop.PropertyType;
                TypeInfo propTypeInfo = propType.GetTypeInfo();
                if (propType == typeof(string) || propTypeInfo.IsValueType)
                {
                    // Ignore terminals
                }
                else if (propTypeInfo.IsSubclassOf(typeof(Node)) || propType == typeof(Node))
                {
                    Node value = (Node)prop.GetValue(node);
                    if (value != null)
                    {
                        InvokeEnterEvent(value);
                        Visit(value);
                        InvokeExitEvent(value);
                    }
                }
                else if (propTypeInfo.ImplementedInterfaces.Contains(typeof(IEnumerable)))
                {
                    Type itemType = propTypeInfo.GenericTypeArguments[0];
                    var collection = (IEnumerable<object>)prop.GetValue(node);
                    if (collection != null)
                    {
                        foreach (var item in collection)
                        {
                            var nodeItem = (Node)item;
                            InvokeEnterEvent(nodeItem);
                            Visit(nodeItem);
                            InvokeExitEvent(nodeItem);
                        }
                    }
                }
                else
                {
                    throw new NotImplementedException($"Property \"{prop}\" processing is not implemented via reflection");
                }
            }
        }

        private void InvokeEnterEvent(object obj)
        {
            var t = obj.GetType();
            Delegate eventDelegate = enterEvents[obj.GetType()];
            eventDelegate?.DynamicInvoke(new object[] { this, obj });
        }

        private void InvokeExitEvent(object obj)
        {
            var t = obj.GetType();
            Delegate eventDelegate = (Delegate)exitEvents[obj.GetType()];
            if (eventDelegate != null)
            {
                eventDelegate.DynamicInvoke(new object[] { this, obj });
            }
        }
    }
}
