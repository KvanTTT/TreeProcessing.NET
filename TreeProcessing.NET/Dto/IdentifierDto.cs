namespace TreeProcessing.NET
{
    public class IdentifierDto : TerminalDto
    {
        public override NodeType NodeType => NodeType.Identifier;

        public string Id { get; set; }

        public IdentifierDto(string id)
        {
            Id = id;
        }

        public IdentifierDto()
        {
        }

        public override string ToString()
        {
            return Id;
        }
    }
}
