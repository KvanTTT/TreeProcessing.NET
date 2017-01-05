using ProtoBuf;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.Statement)]
    [Serializable]
    [ProtoContract]
    [ProtoInclude(50, typeof(BlockStatement))]
    [ProtoInclude(51, typeof(ExpressionStatement))]
    [ProtoInclude(52, typeof(ForStatement))]
    [ProtoInclude(53, typeof(IfElseStatement))]

    [XmlInclude(typeof(BlockStatement))]
    [XmlInclude(typeof(ExpressionStatement))]
    [XmlInclude(typeof(ForStatement))]
    [XmlInclude(typeof(IfElseStatement))]

    [DataContract]
    [KnownType(typeof(BlockStatement))]
    [KnownType(typeof(ExpressionStatement))]
    [KnownType(typeof(ForStatement))]
    [KnownType(typeof(IfElseStatement))]
    public abstract class Statement : Node
    {
        public override NodeType NodeType => NodeType.Statement;

        public Statement()
        {
        }
    }
}
