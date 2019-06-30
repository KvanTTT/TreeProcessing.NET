using ProtoBuf;
using System;
using System.Runtime.Serialization;
using MessagePack;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.Identifier)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    [MessagePackObject]
    public class Identifier : Token
    {
        [IgnoreMember]
        public override NodeType NodeType => NodeType.Identifier;

        [DataMember, ProtoMember(1), Key(0)]
        public string Id { get; set; }

        public Identifier(string id)
        {
            Id = id;
        }

        public Identifier()
        {
        }

        public override int CompareTo(Node other)
        {
            int result = base.CompareTo(other);
            if (result != 0)
            {
                return result;
            }

            Identifier expr = (Identifier)other;
            result = Id.CompareTo(expr.Id);
            if (result != 0)
            {
                return result;
            }

            return 0;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString()
        {
            return Id;
        }

        public override TResult Accept<TResult>(IVisitor<TResult> nodeVisitor)
        {
            return nodeVisitor.Visit(this);
        }
    }
}
