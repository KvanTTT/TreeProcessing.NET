using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.Node)]
#if NET || PORTABLE
    [Serializable]
#endif
    [DataContract]
    [ProtoContract]
    public abstract class Node : IComparable, IComparable<Node>, IEquatable<Node>
    {
        [JsonProperty]
        public abstract NodeType NodeType { get; }

        public bool Equals(Node other)
        {
            return CompareTo(other) == 0;
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as Node);
        }

        public virtual int CompareTo(Node other)
        {
            if (other == null)
            {
                return (int)NodeType;
            }

            if (NodeType != other.NodeType)
            {
                return NodeType - other.NodeType;
            }

            return 0;
        }

        public abstract IEnumerable<Node> Children { get; }

        public abstract IEnumerable<Node> AllDescendants { get; }

        public override int GetHashCode()
        {
            int result = 0;
            foreach (Node child in Children)
            {
                HashUtils.Combine(result, child.GetHashCode());
            }
            return result;
        }
    }
}
