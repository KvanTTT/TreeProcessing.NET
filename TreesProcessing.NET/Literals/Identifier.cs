using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.Identifier)]
    [ProtoContract]
    public class Identifier : Terminal
    {
        public override NodeType NodeType => NodeType.Identifier;

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

        public override string ToString()
        {
            return Id;
        }
    }
}
