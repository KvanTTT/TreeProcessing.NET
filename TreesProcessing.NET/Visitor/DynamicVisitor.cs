using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET
{
    public class DynamicVisitor : IVisitor<Node>
    {
        public virtual Node Visit(Terminal terminal)
        {
            return VisitChild(terminal);
        }

        public virtual Node Visit(Statement statement)
        {
            return VisitChild(statement);
        }

        public virtual Node Visit(Identifier identifier)
        {
            return VisitChild(identifier);
        }

        public virtual Node Visit(Expression expression)
        {
            return VisitChild(expression);
        }

        public virtual Node Visit(Node node)
        {
            return VisitChild(node);
        }

        public virtual Node Visit(BinaryOperatorExpression binaryOperatorExpression)
        {
            return VisitChild(binaryOperatorExpression);
        }

        public virtual Node Visit(BlockStatement blockStatement)
        {
            return VisitChild(blockStatement);
        }

        public virtual Node Visit(BooleanLiteral unaryOperatorExpression)
        {
            return VisitChild(unaryOperatorExpression);
        }

        public virtual Node Visit(ExpressionStatement expressionStatement)
        {
            return VisitChild(expressionStatement);
        }

        public virtual Node Visit(FloatLiteral floatLiteral)
        {
            return VisitChild(floatLiteral);
        }

        public virtual Node Visit(ForStatement forStatement)
        {
            return VisitChild(forStatement);
        }

        public virtual Node Visit(IfElseStatement ifElseStatement)
        {
            return VisitChild(ifElseStatement);
        }

        public virtual Node Visit(IntegerLiteral integerLiteral)
        {
            return VisitChild(integerLiteral);
        }

        public virtual Node Visit(InvocationExpression invocationExpression)
        {
            return VisitChild(invocationExpression);
        }

        public virtual Node Visit(MemberReferenceExpression memberReferenceExpression)
        {
            return VisitChild(memberReferenceExpression);
        }

        public virtual Node Visit(NullLiteral nullLiteral)
        {
            return VisitChild(nullLiteral);
        }

        public virtual Node Visit(StringLiteral stringLiteral)
        {
            return VisitChild(stringLiteral);
        }

        public virtual Node Visit(UnaryOperatorExpression unaryOperatorExpression)
        {
            return VisitChild(unaryOperatorExpression);
        }

        protected Node VisitChild(Node node)
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
