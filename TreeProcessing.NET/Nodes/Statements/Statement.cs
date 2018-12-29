using ProtoBuf;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using MessagePack;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.Statement)]
    [Serializable]
    [ProtoContract]
    [ProtoInclude((int)NodeType.BlockStatement, typeof(BlockStatement))]
    [ProtoInclude((int)NodeType.ExpressionStatement, typeof(ExpressionStatement))]
    [ProtoInclude((int)NodeType.ForStatement, typeof(ForStatement))]
    [ProtoInclude((int)NodeType.IfElseStatement, typeof(IfElseStatement))]

    [XmlInclude(typeof(BlockStatement))]
    [XmlInclude(typeof(ExpressionStatement))]
    [XmlInclude(typeof(ForStatement))]
    [XmlInclude(typeof(IfElseStatement))]

    [DataContract]
    [KnownType(typeof(BlockStatement))]
    [KnownType(typeof(ExpressionStatement))]
    [KnownType(typeof(ForStatement))]
    [KnownType(typeof(IfElseStatement))]

    [MessagePackObject]
    [Union((int)NodeType.BlockStatement, typeof(BlockStatement))]
    [Union((int)NodeType.ExpressionStatement, typeof(ExpressionStatement))]
    [Union((int)NodeType.ForStatement, typeof(ForStatement))]
    [Union((int)NodeType.IfElseStatement, typeof(IfElseStatement))]
    public abstract class Statement : Node
    {
        [IgnoreMember]
        public override NodeType NodeType => NodeType.Statement;

        public Statement()
        {
        }
    }
}
