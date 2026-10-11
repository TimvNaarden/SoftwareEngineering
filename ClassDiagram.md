# Class Diagram

```mermaid
classDiagram
    direction TB

    class Direction {
        <<enumeration>>
        North
        East
        South
        West
    }

    class TurnDirection {
        <<enumeration>>
        Left = -1
        Right = 1
    }

    class Position {
        <<struct>>
        +int X
        +int Y
        +Direction D
        +Position(int x, int y, Direction d)
        +ToString() string
    }

    class ICommand {
        <<interface>>
        +Move(Position p)* Position
        +ToString()* string
        +NumberOfCommands() int
        +MaxNesting(int currentNesting = 0) int
        +NumberOfRepeats() int
    }

    class MoveCommand {
        -int _steps
        +MoveCommand(int steps)
        +Move(Position p) Position
        +ToString() string
    }

    class TurnCommand {
        -TurnDirection _d
        +TurnCommand(TurnDirection d)
        -Turn(Direction direction, TurnDirection turn)$ Direction
        +Move(Position p) Position
        +ToString() string
    }

    class RepeatCommand {
        -int _times
        -List~ICommand~ _commands
        +RepeatCommand(int times, List~ICommand~ commands)
        +Move(Position p) Position
        +ToString() string
        +NumberOfCommands() int
        +MaxNesting(int currentNesting = 0) int
        +NumberOfRepeats() int
    }

    class Program {
        -List~ICommand~ _commands
        -Position Start$
        +Program(List~ICommand~ commands)
        -End() Position
        +ToString() string
        -NumberOfCommands() int
        -MaxNesting() int
        -NumberOfRepeats() int
        +Statistics() string
    }

    class Parser {
        -ParseCommand(string command)$ ICommand
        -ParseRepeat(int times, string commands)$ RepeatCommand
        -ParseInner(string input)$ List~ICommand~
        +Parse(string input)$ Program
        +ParseFile(string path)$ Program
    }

    class UserInput {
        <<static>>
        +AskQuestion(string question)$ string
        +AskQuestion~T~(string question, Dictionary~string, T~ actions, Action onError)$ T
    }

    class Examples {
        <<static>>
        +Program BasicProgram$
        +Program AdvancedProgram$
        +Program ExportProgram$
    }

    class EntryPoint {
        -Dictionary~string, Func~Program~~ exampleProgramSelections$
        -string exampleProgramSelectionPrompt$
        -string metricOrRun$
        -Main(string[] args)$ void
    }

    %% Realization
    ICommand <|.. MoveCommand
    ICommand <|.. TurnCommand
    ICommand <|.. RepeatCommand

    %% Structure
    RepeatCommand o-- "*" ICommand : _commands
    Program o-- "*" ICommand : _commands
    Position --> Direction : D
    TurnCommand --> TurnDirection : _d

    %% Usage
    ICommand ..> Position : moves
    Program ..> Position : Start / End
    Parser ..> ICommand : creates
    Parser ..> Program : creates
    Examples --> Program
    EntryPoint ..> Examples
    EntryPoint ..> Parser
    EntryPoint ..> UserInput
    EntryPoint ..> Program
```
