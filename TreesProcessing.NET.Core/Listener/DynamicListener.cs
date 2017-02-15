using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace TreesProcessing.NET
{
    public class DynamicListener : IListener
    {
        public DynamicListener()
        {
        }

        public void Walk(Node node)
        {
            dynamic dynamicNode = node;
            Enter(dynamicNode);
            VisitChildren(node);
            Exit(dynamicNode);
        }

        public virtual void Enter(Terminal terminal)
        {
        }

        public virtual void Enter(Statement statement)
        {
        }

        public virtual void Enter(Expression expression)
        {
        }

        public virtual void Exit(Expression exrpession)
        {
        }

        public virtual void Enter(BinaryOperatorExpression binaryOperatorExpression)
        {
        }

        public virtual void Enter(MemberReferenceExpression memberReferenceExpression)
        {
        }

        public virtual void Enter(UnaryOperatorExpression unaryOperatorExpression)
        {
        }

        public virtual void Enter(InvocationExpression invocationExpression)
        {
        }

        public virtual void Enter(BooleanLiteral unaryOperatorExpression)
        {
        }

        public virtual void Enter(IntegerLiteral integerLiteral)
        {
        }

        public virtual void Enter(StringLiteral stringLiteral)
        {
        }

        public virtual void Enter(BlockStatement blockStatement)
        {
        }

        public virtual void Enter(ForStatement forStatement)
        {
        }

        public virtual void Enter(IfElseStatement ifElseStatement)
        {
        }

        public virtual void Enter(ExpressionStatement expressionStatement)
        {
        }

        public virtual void Enter(Identifier identifier)
        {
        }

        public virtual void Enter(NullLiteral nullLiteral)
        {
        }

        public virtual void Enter(FloatLiteral floatLiteral)
        {
        }

        public virtual void Enter(Node node)
        {
        }

        public virtual void Exit(BlockStatement blockStatement)
        {
        }

        public virtual void Exit(ExpressionStatement expressionStatement)
        {
        }

        public virtual void Exit(ForStatement forStatement)
        {
        }

        public virtual void Exit(IfElseStatement ifElseStatement)
        {
        }

        public virtual void Exit(BinaryOperatorExpression binaryOperatorExpression)
        {
        }

        public virtual void Exit(MemberReferenceExpression memberReferenceExpression)
        {
        }

        public virtual void Exit(UnaryOperatorExpression unaryOperatorExpression)
        {
        }

        public virtual void Exit(InvocationExpression invocationExpression)
        {
        }

        public virtual void Exit(Statement statement)
        {
        }

        public virtual void Exit(Terminal terminal)
        {
        }

        public virtual void Exit(Node node)
        {
        }

        public virtual void Exit(BooleanLiteral unaryOperatorExpression)
        {
        }

        public virtual void Exit(FloatLiteral floatLiteral)
        {
        }

        public virtual void Exit(IntegerLiteral integerLiteral)
        {
        }

        public virtual void Exit(NullLiteral nullLiteral)
        {
        }

        public virtual void Exit(StringLiteral stringLiteral)
        {
        }

        public virtual void Exit(Identifier identifier)
        {
        }

        private void VisitChildren(Node node)
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
                    dynamic value = prop.GetValue(node);
                    if (value != null)
                    {
                        Enter(value);
                        VisitChildren(value);
                        Exit(value);
                    }
                }
                else if (propTypeInfo.ImplementedInterfaces.Contains(typeof(IEnumerable)))
                {
                    Type itemType = propTypeInfo.GenericTypeArguments[0];
                    var collection = (IList)prop.GetValue(node);
                    if (collection != null)
                    {
                        foreach (var item in collection)
                        {
                            dynamic nodeItem = item;
                            Enter(nodeItem);
                            VisitChildren(nodeItem);
                            Exit(nodeItem);
                        }
                    }
                }
                else
                {
                    throw new NotImplementedException($"Property \"{prop}\" processing is not implemented via reflection");
                }
            }
        }
    }
}
