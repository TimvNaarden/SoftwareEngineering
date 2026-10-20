namespace BasicScratchApp
{
    public interface IFileParser
    {
        public abstract Program ParseFile(string path);
        public abstract Program Parse(string text);
    }
}