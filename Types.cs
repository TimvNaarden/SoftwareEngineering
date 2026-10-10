public enum Direction {
    Up,
    Down,
    Left,
    Right
}

public type TurnDirection = Direction.Left | Direction.Right;

public struct Position {
    public int X;
    public int Y;
    public Direction D;
}

public interface ICommand() {
    public abstract Position Move(Position p);

    public abstract string ToString();
    public virtual int NumberOfCommands() => 1;
}

public class MoveCommand : ICommand {
    private readonly int _steps;

    public MoveCommand(int steps);

    public Position Move(Position p);
    
    public string ToString();
}

public class TurnCommand : ICommand {
    private readonly TurnDirection _d;

    public TurnCommand(TurnDirection d);

    public Position Move(Position p);
    
    public string ToString();
}

public class RepeatCommand : ICommand {
    private readonly int _times;
    private readonly List<ICommand> _commands;

    public RepeatCommand(int times, List<ICommand> commands);

    public Position Move(Position p);
    
    public string ToString();
    public int NumberOfCommands();
}