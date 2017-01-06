namespace TreesProcessing.NET
{
    public abstract class StatementDto : NodeDto
    {
        public override NodeType NodeType => NodeType.Statement;

        public StatementDto()
        {
        }
    }
}
