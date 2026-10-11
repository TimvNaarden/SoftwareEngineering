namespace BasicScratchApp
{
    public static class Examples
    {
        public static Program BasicProgram { get; } = new([
            new MoveCommand(10),
            new TurnCommand(TurnDirection.Right),
            new MoveCommand(10),
            new TurnCommand(TurnDirection.Right),
            new MoveCommand(10),
            new TurnCommand(TurnDirection.Right),
            new MoveCommand(10),
            new TurnCommand(TurnDirection.Right),
        ]);

        public static Program AdvancedProgram { get; } = new([
            new RepeatCommand(4, [
                new MoveCommand(10),
                new TurnCommand(TurnDirection.Right)
            ])
        ]);

        public static Program ExportProgram { get; } = new([
            new MoveCommand(5),
            new TurnCommand(TurnDirection.Left),
            new TurnCommand(TurnDirection.Left),
            new MoveCommand(3),
            new TurnCommand(TurnDirection.Right),
            new RepeatCommand(3, [
                new MoveCommand(1),
                new TurnCommand(TurnDirection.Right),
                new RepeatCommand(5, [
                   new MoveCommand(2),
                ])
            ]),
            new TurnCommand(TurnDirection.Left),
        ]);
    }
}