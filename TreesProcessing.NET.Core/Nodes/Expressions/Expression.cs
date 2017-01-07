using ProtoBuf;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.Expression)]
#if !CORE
    [Serializable]
#endif
    [ProtoContract]
    [ProtoInclude(10, typeof(BinaryOperatorExpression))]
    [ProtoInclude(11, typeof(InvocationExpression))]
    [ProtoInclude(12, typeof(MemberReferenceExpression))]
    [ProtoInclude(13, typeof(UnaryOperatorExpression))]
    [ProtoInclude(20, typeof(BooleanLiteral))]
    [ProtoInclude(21, typeof(FloatLiteral))]
    [ProtoInclude(22, typeof(Identifier))]
    [ProtoInclude(23, typeof(IntegerLiteral))]
    [ProtoInclude(24, typeof(NullLiteral))]
    [ProtoInclude(25, typeof(StringLiteral))]

    [XmlInclude(typeof(BinaryOperatorExpression))]
    [XmlInclude(typeof(InvocationExpression))]
    [XmlInclude(typeof(MemberReferenceExpression))]
    [XmlInclude(typeof(UnaryOperatorExpression))]
    [XmlInclude(typeof(BooleanLiteral))]
    [XmlInclude(typeof(FloatLiteral))]
    [XmlInclude(typeof(Identifier))]
    [XmlInclude(typeof(IntegerLiteral))]
    [XmlInclude(typeof(NullLiteral))]
    [XmlInclude(typeof(StringLiteral))]

    [DataContract]
    [KnownType(typeof(BinaryOperatorExpression))]
    [KnownType(typeof(InvocationExpression))]
    [KnownType(typeof(MemberReferenceExpression))]
    [KnownType(typeof(UnaryOperatorExpression))]
    [KnownType(typeof(BooleanLiteral))]
    [KnownType(typeof(FloatLiteral))]
    [KnownType(typeof(Identifier))]
    [KnownType(typeof(IntegerLiteral))]
    [KnownType(typeof(NullLiteral))]
    [KnownType(typeof(StringLiteral))]
    public abstract class Expression : Node
    {
        public override NodeType NodeType => NodeType.Expression;

        public Expression()
        {
        }
    }
}
