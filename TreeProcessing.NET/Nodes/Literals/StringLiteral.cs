﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TreeProcessing.NET
{
    [NodeAttr(NodeType.StringLiteral)]
#if PORTABLE || NET
    [Serializable]
#endif
    [DataContract]
    [ProtoContract]
    public class StringLiteral : Token
    {
        public override NodeType NodeType => NodeType.StringLiteral;

        [DataMember]
        [ProtoMember(1)]
        public string Value { get; set; }

        public StringLiteral(string value)
        {
            Value = value;
        }

        public StringLiteral()
        {
        }

        public override int CompareTo(Node other)
        {
            int result = base.CompareTo(other);
            if (result != 0)
            {
                return result;
            }

            StringLiteral expr = (StringLiteral)other;
            result = Value.CompareTo(expr.Value);
            if (result != 0)
            {
                return result;
            }

            return 0;
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString()
        {
            return Value;
        }
    }
}