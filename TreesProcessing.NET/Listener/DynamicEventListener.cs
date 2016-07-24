using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET
{
    public class DynamicEventListener : IEventListener
    {
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

        public void Walk(Node node)
        {
            throw new NotImplementedException();
        }

        /*private void Visit(Node node)
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
                TypeInfo typeInfo = propType.GetTypeInfo();
                if (typeInfo.IsSubclassOf(typeof(Node)) || propType == typeof(Node))
                {
                    Node value = (Node)prop.GetValue(node);
                    Enter(value);
                    Visit(value);
                    Exit(value);
                }
                else if (typeInfo.ImplementedInterfaces.Contains(typeof(IEnumerable)))
                {
                    Type itemType = typeInfo.GenericTypeArguments[0];
                    var collection = (IEnumerable<object>)prop.GetValue(node);
                    if (collection != null)
                    {
                        foreach (var item in collection)
                        {
                            var nodeItem = item as Node;
                            if (nodeItem != null)
                            {
                                Enter(nodeItem);
                                Visit(nodeItem);
                                Exit(nodeItem);
                            }
                        }
                    }
                }
                else
                {
                    throw new NotImplementedException($"Property \"{prop}\" processing is not implemented via reflection");
                }
            }
        }*/
    }
}
