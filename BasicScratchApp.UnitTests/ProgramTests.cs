
namespace BasicScratchApp.UnitTests
{
    public class ProgramTests
    {
        [Fact]
        public void BasicProgramStatisticsTest()
        {
            Assert.Equal(
                "Number of commands: 8\nMax nesting: 0\nNumber of repeats: 0",
                Examples.BasicProgram.Statistics()
            );
        }

        [Fact]
        public void AdvancedProgramStatisticsTest()
        {
            Assert.Equal(
                "Number of commands: 3\nMax nesting: 1\nNumber of repeats: 1",
                Examples.AdvancedProgram.Statistics()
            );
        }

        [Fact]
        public void ExpertProgramStatisticsTest()
        {
            Assert.Equal(
                "Number of commands: 11\nMax nesting: 2\nNumber of repeats: 2",
                Examples.ExpertProgram.Statistics()
            );
        }
        
        [Fact]
        public void BasicProgramToStringTest()
        {
            Assert.Equal(
                "Move 10, Turn Right, Move 10, Turn Right, Move 10, Turn Right, Move 10, Turn Right.\n(0, 0) facing East",
                Examples.BasicProgram.ToString()
            );
        }

        [Fact]
        public void AdvancedProgramToStringTest()
        {
            Assert.Equal(
                "Move 10, Turn Right, Move 10, Turn Right, Move 10, Turn Right, Move 10, Turn Right.\n(0, 0) facing East",
                Examples.AdvancedProgram.ToString()
            );
        }

        [Fact]
        public void ExpertProgramToStringTest()
        {
            Assert.Equal(
                "Move 5, Turn Left, Turn Left, Move 3, Turn Right, "
                    + "Move 1, Turn Right, Move 2, Move 2, Move 2, Move 2, Move 2, "
                    + "Move 1, Turn Right, Move 2, Move 2, Move 2, Move 2, Move 2, "
                    + "Move 1, Turn Right, Move 2, Move 2, Move 2, Move 2, Move 2, "
                    + "Turn Left.\n(3, -10) facing South",
                Examples.ExpertProgram.ToString()
            );
        }

        [Fact]
        public void MoveZeroProgramToStringTest()
        {
            Program program = new Program([new MoveCommand(0)]);
            Assert.Equal(
                "Move 0.\n(0, 0) facing East",
                program.ToString()
            );
        }

        [Fact]
        public void TurnOnlyProgramToStringTest()
        {
            Program program = new Program([
                new TurnCommand(TurnDirection.Left),
                new TurnCommand(TurnDirection.Left)
            ]);
            Assert.Equal(
                "Turn Left, Turn Left.\n(0, 0) facing West",
                program.ToString()
            );
        }

        [Fact]
        public void RepeatProgramToStringTest()
        {
            Program program = new Program([
                new RepeatCommand(2, [
                    new MoveCommand(3),
                    new TurnCommand(TurnDirection.Left)
                ])
            ]);
            Assert.Equal(
                "Move 3, Turn Left, Move 3, Turn Left.\n(3, 3) facing West",
                program.ToString()
            );
        }

        [Fact]
        public void NestedRepeatProgramStatisticsTest()
        {
            Program program = new Program([
                new RepeatCommand(2, [
                    new RepeatCommand(2, [
                        new RepeatCommand(2, [
                            new MoveCommand(1)
                        ])
                    ])
                ])
            ]);
            Assert.Equal(
                "Number of commands: 4\nMax nesting: 3\nNumber of repeats: 3",
                program.Statistics()
            );
        }

        // Max nesting takes the deepest repeat, the number of repeats counts all of them
        [Fact]
        public void SeparateRepeatsProgramStatisticsTest()
        {
            Program program = new Program([
                new RepeatCommand(2, [
                    new MoveCommand(1)
                ]),
                new RepeatCommand(3, [
                    new TurnCommand(TurnDirection.Left),
                    new RepeatCommand(2, [
                        new MoveCommand(1)
                    ])
                ])
            ]);
            Assert.Equal(
                "Number of commands: 6\nMax nesting: 2\nNumber of repeats: 3",
                program.Statistics()
            );
        }

    }
}
