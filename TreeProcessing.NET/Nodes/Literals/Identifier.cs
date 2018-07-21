using ProtoBuf;
using System;
using System.Runtime.Serialization;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.Identifier)]
#if PORTABLE || NET
    [Serializable]
#endif
    [DataContract]
    [ProtoContract]
    public class Identifier : Token
    {
        public override NodeType NodeType => NodeType.Identifier;

        [DataMember]
        [ProtoMember(1)]
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
    }
}
