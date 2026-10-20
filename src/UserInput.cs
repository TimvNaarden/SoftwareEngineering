namespace BasicScratchApp
{
    public static class UserInput
    {
        public static string AskQuestion(string question)
        {
            Console.Write(question);
            var input = Console.ReadLine();
            if (input != null) return input;
            else throw new ArgumentException("Input was null");
        }

        // When a user selects a non-existing option, throws KeyNotFound
        public static T AskQuestion<T>(string question, Dictionary<string, T> actions)
        {
            Console.Write(question);
            var input = Console.ReadLine();
            if (input != null && actions.TryGetValue(input.Trim(), out T? value))
            {
                return value;
            }
            else
            {
                throw new KeyNotFoundException("Input not recognized");
            }
        }
    }
}