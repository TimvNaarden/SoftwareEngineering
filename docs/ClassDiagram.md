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
        -Dictionary~string, IFileParser~ _parsers$
        +ParseFile(string path)$ Program
        +Parse(string src, string type)$ Program
    }

    class IFileParser {
        <<interface>>
        +ParseFile(string path)* Program
        +Parse(string text)* Program
    }

    class TextParser {
        -ParseCommand(string command)$ ICommand
        -ParseRepeat(int times, string commands)$ RepeatCommand
        -ParseInner(string input)$ List~ICommand~
        +Parse(string input) Program
        +ParseFile(string path) Program
    }

    class UserInput {
        <<static>>
        +AskQuestion(string question)$ string
        +AskQuestion~T~(string question, Dictionary~string, T~ actions)$ T
    }

    class Examples {
        <<static>>
        +Program BasicProgram$
        +Program AdvancedProgram$
        +Program ExpertProgram$
    }

    class EntryPoint {
        -Dictionary~string, Func~Program~~ exampleProgramSelections$
        -string exampleProgramSelectionPrompt$
        -string metricOrRun$
        -Main()$ void
    }

    %% Realization
    ICommand <|.. MoveCommand
    ICommand <|.. TurnCommand
    ICommand <|.. RepeatCommand
    IFileParser <|.. TextParser

    %% Structure
    RepeatCommand o-- "*" ICommand : _commands
    Program o-- "*" ICommand : _commands
    Position --> Direction : D
    TurnCommand --> TurnDirection : _d
    Parser o-- "*" IFileParser : _parsers

    %% Usage
    ICommand ..> Position : moves
    Program ..> Position : Start / End
    Parser ..> Program : returns
    TextParser ..> ICommand : creates
    TextParser ..> Program : creates
    Examples --> Program
    EntryPoint ..> Examples
    EntryPoint ..> Parser
    EntryPoint ..> UserInput
    EntryPoint ..> Program
```

# Classes

## ICommand

Every command with the Move function, that applies the command to the current position.
The `NumberOfCommands`, `MaxNesting` and `NumberOfRepeats` functions are only there for the nested case that exists for repeat commands.

## Program

Contains a sequence of commands that can either be executed to obtain the final positon or to be analyzed for the metrics.
Has a Position start, which can be used/ changed in the future to assign a dynamic starting position.

## Examples

Static class that contains three basic programs, defined here, rather then cluttering the main class.

## UserInput

Abstraction layer between the user and the program. Can ask questions and get an input or couple the input to an action directly.

## Parser

Entry point for parsing files. Keeps a dictionary that maps a file extension to an `IFileParser` and passes the file to the parser for that extension.
Throws a `KeyNotFoundException` when the extension is not supported. A new file format can be added by implementing `IFileParser` and registering it in `_parsers`.

## IFileParser

Interface for a parser of one file format. `ParseFile` reads the file from a path, `Parse` turns the file contents into a `Program`.

## TextParser

Implementation of `IFileParser` for `.txt` files, that have the following styling rules:

- Repeats nested by 4 spaces
- White lines ignored
- One space between command and property with nothing following afterwards
- Ran top to bottom

## EntryPoint

Basic C# entrypoint that just asks the user for their desired action.
