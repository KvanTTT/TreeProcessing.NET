namespace TreeProcessing.NET
{
    public abstract class TerminalDto : ExpressionDto
    {
        public override NodeType NodeType => NodeType.Terminal;
    }
}
