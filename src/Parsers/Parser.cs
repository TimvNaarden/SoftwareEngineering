namespace BasicScratchApp
{
    public class Parser
    {
        private static Dictionary<string, IFileParser> _parsers = new()
        {
            {"txt", new TextParser()},
        };

        public static Program ParseFile(string path)
        {
            string[] parts = path.Split(".");
            if (!_parsers.TryGetValue(parts.Last(), out IFileParser? parser))
            {
                throw new KeyNotFoundException("Extension not supported: " + parts.Last());
            }
            return parser.ParseFile(path);
        }

        public static Program Parse(string src, string type)
        {
            return ParseFile(src + "." + type);
        }

    }
}
