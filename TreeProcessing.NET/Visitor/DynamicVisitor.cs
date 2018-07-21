using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TreeProcessing.NET
{
    public class DynamicVisitor<T> : IVisitor<T>
    {
        public virtual T Visit(Token terminal)
        {
            return VisitChildren(terminal);
        }

        public virtual T Visit(Statement statement)
        {
            return VisitChildren(statement);
        }

        public virtual T Visit(Identifier identifier)
        {
            return VisitChildren(identifier);
        }

        public virtual T Visit(Expression expression)
        {
            return VisitChildren(expression);
        }

        public virtual T Visit(Node node)
        {
            return VisitChildren(node);
        }

        public virtual T Visit(BinaryOperatorExpression binaryOperatorExpression)
        {
            return VisitChildren(binaryOperatorExpression);
        }

        public virtual T Visit(BlockStatement blockStatement)
        {
            return VisitChildren(blockStatement);
        }

        public virtual T Visit(BooleanLiteral unaryOperatorExpression)
        {
            return VisitChildren(unaryOperatorExpression);
        }

        public virtual T Visit(ExpressionStatement expressionStatement)
        {
            return VisitChildren(expressionStatement);
        }

        public virtual T Visit(FloatLiteral floatLiteral)
        {
            return VisitChildren(floatLiteral);
        }

        public virtual T Visit(ForStatement forStatement)
        {
            return VisitChildren(forStatement);
        }

        public virtual T Visit(IfElseStatement ifElseStatement)
        {
            return VisitChildren(ifElseStatement);
        }

        public virtual T Visit(IntegerLiteral integerLiteral)
        {
            return VisitChildren(integerLiteral);
        }

        public virtual T Visit(InvocationExpression invocationExpression)
        {
            return VisitChildren(invocationExpression);
        }

        public virtual T Visit(MemberReferenceExpression memberReferenceExpression)
        {
            return VisitChildren(memberReferenceExpression);
        }

        public virtual T Visit(NullLiteral nullLiteral)
        {
            return VisitChildren(nullLiteral);
        }

        public virtual T Visit(StringLiteral stringLiteral)
        {
            return VisitChildren(stringLiteral);
        }

        public virtual T Visit(UnaryOperatorExpression unaryOperatorExpression)
        {
            return VisitChildren(unaryOperatorExpression);
        }

        public virtual T VisitChildren(Node node)
        {
            if (node == null)
            {
                return DefaultResult;
            }

            Type type = node.GetType();
            var result = (Node)Activator.CreateInstance(type);
            PropertyInfo[] properties = ReflectionCache.GetClassProperties(type);
            foreach (PropertyInfo prop in properties)
            {
                Type propType = prop.PropertyType;
                TypeInfo typeInfo = propType.GetTypeInfo();
                if (!typeInfo.IsValueType && propType != typeof(string))
                {
                    if (typeInfo.IsSubclassOf(typeof(Node)) || propType == typeof(Node))
                    {
                        Node getValue = (Node)prop.GetValue(node);
                        if (getValue != null)
                        {
                            Visit((dynamic)getValue);
                        }
                    }
                    else if (typeInfo.ImplementedInterfaces.Contains(typeof(IEnumerable)))
                    {
                        Type itemType = typeInfo.GenericTypeArguments[0];
                        var sourceCollection = (IEnumerable<object>)prop.GetValue(node);

                        if (sourceCollection != null)
                        {
                            foreach (var item in sourceCollection)
                            {
                                var nodeItem = item as Node;
                                if (nodeItem != null)
                                {
                                    Visit((dynamic)nodeItem);
                                }
                            }
                        }
                    }
                }
            }

            return DefaultResult;
        }

        public virtual T DefaultResult => default(T);
    }
}
