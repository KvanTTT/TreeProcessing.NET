namespace TreeProcessing.NET
{
    public class StringLiteralDto : TerminalDto
    {
        public override NodeType NodeType => NodeType.StringLiteral;

        public string Value { get; set; }

        public StringLiteralDto(string value)
        {
            Value = value;
        }

        public StringLiteralDto()
        {
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
