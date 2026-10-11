namespace BasicScratchApp
{
    public enum Direction { North, East, South, West }

    public enum TurnDirection { Left = -1, Right = 1 } // Clockwise or counter clockwise

    public struct Position(int x, int y, Direction d)
    {
        public int X = x;
        public int Y = y;
        public Direction D = d;

        public readonly override string ToString()
        {
            return $"({X}, {Y}) facing {D}";
        }
    }

    public interface ICommand
    {
        public abstract Position Move(Position p);

        public abstract string ToString();
        public virtual int NumberOfCommands() => 1;
        public virtual int MaxNesting(int currentNesting = 0) => currentNesting;
        public virtual int NumberOfRepeats() => 0;
    }

    public class MoveCommand(int steps) : ICommand
    {
        private readonly int _steps = steps;

        public Position Move(Position p)
        {
            return p.D switch
            {
                Direction.North => new Position(p.X, p.Y + _steps, p.D),
                Direction.South => new Position(p.X, p.Y - _steps, p.D),
                Direction.West => new Position(p.X - _steps, p.Y, p.D),
                Direction.East => new Position(p.X + _steps, p.Y, p.D),
                _ => throw new ArgumentException("Non Existing direction"),
            };
        }

        public override string ToString() => "Move " + _steps.ToString();
    }

    public class TurnCommand(TurnDirection d) : ICommand
    {
        private readonly TurnDirection _d = d;

        private static Direction Turn(Direction direction, TurnDirection turn)
        {
            return (Direction)(((int)direction + (int)turn + 4) % 4);
        }

        public Position Move(Position p)
        {
            return new Position(p.X, p.Y, Turn(p.D, _d));
        }

        public override string ToString() => "Turn " + _d;
    }

    public class RepeatCommand(int times, List<ICommand> commands) : ICommand
    {
        private readonly int _times = times;
        private readonly List<ICommand> _commands = commands;

        public Position Move(Position p)
        {
            Position res = p;
            for (int i = 0; i < _times; i++)
                foreach (ICommand com in _commands) res = com.Move(res);
            return res;
        }

        public override string ToString()
        {
            string res = "";
            foreach (ICommand com in _commands)
            {
                if (res != "") res += ", ";
                res += com.ToString();
            }

            string resRepeated = "";
            for (int i = 1; i <= _times; i++)
            {
                if (resRepeated != "") resRepeated += ", ";
                resRepeated += res;
            }

            return resRepeated;
        }
        public int NumberOfCommands()
        {
            int res = 1;
            foreach (ICommand com in _commands) res += com.NumberOfCommands();
            return res;
        }

        public int MaxNesting(int currentNesting = 0)
        {
            int maxNesting = currentNesting + 1;
            foreach (ICommand com in _commands)
            {
                int nesting = com.MaxNesting(currentNesting + 1);
                if (nesting > maxNesting) maxNesting = nesting;
            }
            return maxNesting;
        }

        public int NumberOfRepeats()
        {
            int res = 1;
            foreach (ICommand com in _commands) res += com.NumberOfRepeats();
            return res;
        }
    }
}