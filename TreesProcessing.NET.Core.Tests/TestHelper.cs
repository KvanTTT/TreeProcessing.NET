namespace TreesProcessing.NET.Tests
{
    public class TestHelper
    {
        public const string Platform =
#if CORE
            "CORE"
#elif PORTABLE
            "PORTABLE"
#else
            "NET"
#endif
            ;
    }
}
