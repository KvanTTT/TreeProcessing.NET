using ProtoBuf;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using MessagePack;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.Expression)]
    [Serializable]
    [ProtoContract]
    [ProtoInclude((int)NodeType.BinaryOperatorExpression, typeof(BinaryOperatorExpression))]
    [ProtoInclude((int)NodeType.InvocationExpression, typeof(InvocationExpression))]
    [ProtoInclude((int)NodeType.MemberReferenceExpression, typeof(MemberReferenceExpression))]
    [ProtoInclude((int)NodeType.UnaryOperatorExpression, typeof(UnaryOperatorExpression))]
    [ProtoInclude((int)NodeType.BooleanLiteral, typeof(BooleanLiteral))]
    [ProtoInclude((int)NodeType.FloatLiteral, typeof(FloatLiteral))]
    [ProtoInclude((int)NodeType.Identifier, typeof(Identifier))]
    [ProtoInclude((int)NodeType.IntegerLiteral, typeof(IntegerLiteral))]
    [ProtoInclude((int)NodeType.NullLiteral, typeof(NullLiteral))]
    [ProtoInclude((int)NodeType.StringLiteral, typeof(StringLiteral))]

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

    [MessagePackObject]
    [Union((int)NodeType.BinaryOperatorExpression, typeof(BinaryOperatorExpression))]
    [Union((int)NodeType.InvocationExpression, typeof(InvocationExpression))]
    [Union((int)NodeType.MemberReferenceExpression, typeof(MemberReferenceExpression))]
    [Union((int)NodeType.UnaryOperatorExpression, typeof(UnaryOperatorExpression))]
    [Union((int)NodeType.BooleanLiteral, typeof(BooleanLiteral))]
    [Union((int)NodeType.FloatLiteral, typeof(FloatLiteral))]
    [Union((int)NodeType.Identifier, typeof(Identifier))]
    [Union((int)NodeType.IntegerLiteral, typeof(IntegerLiteral))]
    [Union((int)NodeType.NullLiteral, typeof(NullLiteral))]
    [Union((int)NodeType.StringLiteral, typeof(StringLiteral))]
    public abstract class Expression : Node
    {
        [IgnoreMember]
        public override NodeType NodeType => NodeType.Expression;

        public Expression()
        {
        }
    }
}
