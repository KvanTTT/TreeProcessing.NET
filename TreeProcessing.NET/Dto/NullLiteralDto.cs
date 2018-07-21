namespace TreeProcessing.NET
{
    public class NullLiteralDto : TerminalDto
    {
        public override NodeType NodeType => NodeType.NullLiteral;

        public NullLiteralDto()
        {
        }

        public override string ToString()
        {
            return "null";
        }
    }
}
