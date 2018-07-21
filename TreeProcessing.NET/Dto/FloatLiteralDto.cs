namespace TreeProcessing.NET
{
    public class FloatLiteralDto : TerminalDto
    {
        public override NodeType NodeType => NodeType.FloatLiteral;

        public float Value { get; set; }

        public FloatLiteralDto(float value)
        {
            Value = value;
        }

        public FloatLiteralDto()
        {
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
