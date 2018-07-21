namespace TreeProcessing.NET
{
    // Source: https://github.com/dotnet/roslyn
    public class HashUtils
    {
        public static int Combine(int newKey, int currentKey)
        {
            return unchecked((currentKey * (int)0xA5555529) + newKey);
        }
    }
}
