using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TreeProcessing.NET
{
    public class DynamicVisitor : IVisitor<Node>
    {
        public virtual Node Visit(Terminal terminal)
        {
            return VisitChildren(terminal);
        }

        public virtual Node Visit(Statement statement)
        {
            return VisitChildren(statement);
        }

        public virtual Node Visit(Identifier identifier)
        {
            return VisitChildren(identifier);
        }

        public virtual Node Visit(Expression expression)
        {
            return VisitChildren(expression);
        }

        public virtual Node Visit(Node node)
        {
            return VisitChildren(node);
        }

        public virtual Node Visit(BinaryOperatorExpression binaryOperatorExpression)
        {
            return VisitChildren(binaryOperatorExpression);
        }

        public virtual Node Visit(BlockStatement blockStatement)
        {
            return VisitChildren(blockStatement);
        }

        public virtual Node Visit(BooleanLiteral unaryOperatorExpression)
        {
            return VisitChildren(unaryOperatorExpression);
        }

        public virtual Node Visit(ExpressionStatement expressionStatement)
        {
            return VisitChildren(expressionStatement);
        }

        public virtual Node Visit(FloatLiteral floatLiteral)
        {
            return VisitChildren(floatLiteral);
        }

        public virtual Node Visit(ForStatement forStatement)
        {
            return VisitChildren(forStatement);
        }

        public virtual Node Visit(IfElseStatement ifElseStatement)
        {
            return VisitChildren(ifElseStatement);
        }

        public virtual Node Visit(IntegerLiteral integerLiteral)
        {
            return VisitChildren(integerLiteral);
        }

        public virtual Node Visit(InvocationExpression invocationExpression)
        {
            return VisitChildren(invocationExpression);
        }

        public virtual Node Visit(MemberReferenceExpression memberReferenceExpression)
        {
            return VisitChildren(memberReferenceExpression);
        }

        public virtual Node Visit(NullLiteral nullLiteral)
        {
            return VisitChildren(nullLiteral);
        }

        public virtual Node Visit(StringLiteral stringLiteral)
        {
            return VisitChildren(stringLiteral);
        }

        public virtual Node Visit(UnaryOperatorExpression unaryOperatorExpression)
        {
            return VisitChildren(unaryOperatorExpression);
        }

        protected Node VisitChildren(Node node)
        {
            if (node == null)
            {
                return null;
            }

            Type type = node.GetType();
            var result = (Node)Activator.CreateInstance(type);
            PropertyInfo[] properties = ReflectionCache.GetClassProperties(type);
            foreach (PropertyInfo prop in properties)
            {
                Type propType = prop.PropertyType;
                TypeInfo typeInfo = propType.GetTypeInfo();
                if (typeInfo.IsValueType)
                {
                    prop.SetValue(result, prop.GetValue(node));
                }
                else if (propType == typeof(string))
                {
                    string stringValue = (string)prop.GetValue(node);
                    prop.SetValue(result, stringValue != null ? (string)prop.GetValue(node) : null);
                }
                else if (typeInfo.IsSubclassOf(typeof(Node)) || propType == typeof(Node))
                {
                    Node getValue = (Node)prop.GetValue(node);
                    Node setValue = getValue != null ? Visit(getValue) : null;
                    prop.SetValue(result, setValue);
                }
                else if (typeInfo.ImplementedInterfaces.Contains(typeof(IEnumerable)))
                {
                    Type itemType = typeInfo.GenericTypeArguments[0];
                    var sourceCollection = (IEnumerable<object>)prop.GetValue(node);
                    IList destCollection = null;
                    if (sourceCollection != null)
                    {
                        destCollection = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType));
                        foreach (var item in sourceCollection)
                        {
                            var nodeItem = item as Node;
                            if (nodeItem != null)
                            {
                                destCollection.Add(Visit(nodeItem));
                            }
                            else
                            {
                                destCollection.Add(item);
                            }
                        }
                    }
                    prop.SetValue(result, destCollection);
                }
                else
                {
                    throw new NotImplementedException($"Property \"{prop}\" processing is not implemented via reflection");
                }
            }

            return result;
        }
    }
}
