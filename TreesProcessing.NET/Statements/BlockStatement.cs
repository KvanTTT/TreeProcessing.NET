using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreesProcessing.NET
{
    [NodeAttr(NodeType.BlockStatement)]
    [ProtoContract]
    public class BlockStatement : Statement
    {
        public override NodeType NodeType => NodeType.BlockStatement;

        public IList<Statement> Statements { get; set; }

        [JsonIgnore]
        [ProtoMember(1, Name = nameof(Statements), OverwriteList = true)]
        public IList<Node> StatementsSerializable
        {
            get { return Statements.Select(s => (Node)s).ToList(); }
            set { Statements = value.Select(s => (Statement)s).ToList(); }
        }

        public BlockStatement(IList<Statement> statements)
        {
            Statements = statements;
        }

        public BlockStatement()
        {
        }

        public override int CompareTo(Node other)
        {
            int result = base.CompareTo(other);
            if (result != 0)
            {
                return result;
            }

            BlockStatement statement = (BlockStatement)other;
            if (Statements.Count != statement.Statements.Count)
            {
                return Statements.Count - statement.Statements.Count;
            }

            for (int i = 0; i < Statements.Count; i++)
            {
                result = Statements[i].CompareTo(statement.Statements[i]);
                if (result != 0)
                {
                    return result;
                }
            }

            return 0;
        }

        public override IEnumerable<Node> Descendants
        {
            get
            {
                var result = new List<Node>();
                foreach (var statement in Statements)
                {
                    result.Add(statement);
                    result.AddRange(statement.Descendants);
                }
                return result;
            }
        }

        public override string ToString()
        {
            return string.Join(" ", Statements);
        }
    }
}
