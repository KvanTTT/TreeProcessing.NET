namespace TreeProcessing.NET
{
    public class IntegerLiteralDto : TerminalDto
    {
        public override NodeType NodeType => NodeType.IntegerLiteral;

        public int Value { get; set; }

        public IntegerLiteralDto(int value)
        {
            Value = value;
        }

        public IntegerLiteralDto()
        {
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
