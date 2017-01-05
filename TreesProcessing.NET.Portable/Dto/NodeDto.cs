using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.Node)]
    [Serializable]
    [DataContract]
    [ProtoContract]
    public abstract class NodeDto
    {
        [JsonProperty]
        public abstract NodeType NodeType { get; }
    }
}
