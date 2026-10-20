namespace BasicScratchApp
{
    public class TextParser() : IFileParser
    {
        private static ICommand ParseCommand(string command)
        {
            if (command.StartsWith("Move"))
            {
                var parts = command.Split(' ', 3);
                var steps = int.Parse(parts[1]);
                return new MoveCommand(steps);
            }
            else if (command.StartsWith("Turn"))
            {
                var parts = command.Split(' ', 3);
                var direction = parts[1] == "Left" ? TurnDirection.Left : TurnDirection.Right;
                return new TurnCommand(direction);
            }
            // Repeat parsed in it's s own function
            else
            {
                throw new ArgumentException($"Unknown command: {command}");
            }
        }
        private static RepeatCommand ParseRepeat(int times, string commands)
        {
            List<ICommand> subCommands = ParseInner(commands);
            return new RepeatCommand(times, subCommands);
        }
        private static List<ICommand> ParseInner(string input)
        {
            List<ICommand> commands = [];
            string subCommands = "";
            int repeatCount = 0;
            string[] lines = input.Split('\n');
            foreach (var line in lines)
            {
                if (line.StartsWith("    ")) // In repeat
                {
                    subCommands += string.Concat(line.AsSpan(4), "\n");
                }
                else if (subCommands != "") // Repeat stopped
                {
                    var command = ParseRepeat(repeatCount, subCommands);
                    commands.Add(command);
                    subCommands = "";
                    repeatCount = 0;
                }
                else if (line.StartsWith("Repeat")) // Start repeat
                {
                    var parts = line.Split(' ', 3);
                    repeatCount = int.Parse(parts[1]);
                }
                else if (line.Trim() != "") // Non white line -> normal command
                {
                    var command = ParseCommand(line.Trim());
                    commands.Add(command);
                }
            }
            if (subCommands != "") // Catch when last command is in repeat
            {
                var command = ParseRepeat(repeatCount, subCommands);
                commands.Add(command);
            }

            return commands;
        }
        public Program Parse(string input)
        {
            return new Program(ParseInner(input));
        }

        public Program ParseFile(string path)
        {
            var input = File.ReadAllText(path);
            return Parse(input);
        }
    }
}