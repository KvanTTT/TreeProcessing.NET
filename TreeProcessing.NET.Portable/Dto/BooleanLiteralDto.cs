namespace TreeProcessing.NET
{
    public class BooleanLiteralDto : TerminalDto
    {
        public override NodeType NodeType => NodeType.BooleanLiteral;

        public bool Value { get; set; }

        public BooleanLiteralDto(bool value)
        {
            Value = value;
        }

        public BooleanLiteralDto()
        {
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
