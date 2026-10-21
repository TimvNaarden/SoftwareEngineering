namespace BasicScratchApp
{
    public class Program(List<ICommand> commands)
    {
        private readonly List<ICommand> _commands = commands;

        // Assumes everything starts at (0, 0) facing East
        private static Position Start => new(0, 0, Direction.East);
        private Position End()
        {
            Position res = Start;
            foreach (ICommand com in _commands) res = com.Move(res);
            return res;
        }

        // Format from the assignment:
        // [Commands] joined by ", " and followed by a period. 
        // Final Position on a new line in the format (x, y) facing Direction.
        public override string ToString()
        {
            string[] commands = [.. _commands.Select(com => com.ToString())];
            string res = string.Join(", ", commands) + ".\n";
            return res + End().ToString();
        }
        private int NumberOfCommands()
        {
            int[] numberOfCommands = [.. _commands.Select(com => com.NumberOfCommands())];
            return numberOfCommands.Sum();
        }

        private int MaxNesting()
        {
            int[] maxNesting = [.. _commands.Select(com => com.MaxNesting())];
            return maxNesting.Max();
        }

        private int NumberOfRepeats()
        {
            int[] numberOfRepeats = [.. _commands.Select(com => com.NumberOfRepeats())];
            return numberOfRepeats.Sum();
        }

        // From the assignment: 
        // Number of commands: x
        // Max nesting: y
        // Number of repeats: z
        public string Statistics()
        {
            return $"Number of commands: {NumberOfCommands()}\nMax nesting: {MaxNesting()}\nNumber of repeats: {NumberOfRepeats()}";
        }
    }
}




