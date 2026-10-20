using System.Runtime.CompilerServices;

namespace BasicScratchApp.UnitTests
{
    public class CommandsTests
    {
        // Test if 0 steps does not move
        [Fact]
        public void MoveCommandTestZero()
        {
            MoveCommand move = new MoveCommand(0);
            Position start = new Position(0, 10, Direction.East);
            Position end = move.Move(start);
            Assert.Equal(new Position(0, 10, Direction.East), end);
        }

        // Test moving 10 steps in all directions
        [Theory]
        [InlineData(10, Direction.North, 10, 20)]
        [InlineData(10, Direction.South, 10, 0)]
        [InlineData(10, Direction.West, 0, 10)]
        [InlineData(10, Direction.East, 20, 10)]
        public void MoveCommandTestAllDirections(int steps, Direction direction, int expectedX, int expectedY)
        {
            Position expected = new Position(expectedX, expectedY, direction);
            MoveCommand move = new MoveCommand(steps);
            Position start = new Position(10, 10, direction);
            Position end = move.Move(start);
            Assert.Equal(expected, end);
        }

        // Test turning left and right from all directions
        [Theory]
        [InlineData(Direction.North, TurnDirection.Left, Direction.West)]
        [InlineData(Direction.North, TurnDirection.Right, Direction.East)]
        [InlineData(Direction.South, TurnDirection.Left, Direction.East)]
        [InlineData(Direction.South, TurnDirection.Right, Direction.West)]
        [InlineData(Direction.West, TurnDirection.Left, Direction.South)]
        [InlineData(Direction.West, TurnDirection.Right, Direction.North)]
        [InlineData(Direction.East, TurnDirection.Left, Direction.North)]
        [InlineData(Direction.East, TurnDirection.Right, Direction.South)]
        public void TurnCommandTest(Direction startD, TurnDirection turn, Direction endD)
        {
            TurnCommand turnCommand = new TurnCommand(turn);
            Position start = new Position(-10, 10 , startD);
            Position end = turnCommand.Move(start);
            Assert.Equal(new Position(-10, 10, endD), end);
        }

        // This test checks if the RepeatCommand executes moving 10 and then turning left 3 times in repeat correctly.
        [Fact]
        public void RepeatCommandTest()
        {
            RepeatCommand repeat = new RepeatCommand(3, [
                new MoveCommand(10), 
                new TurnCommand(TurnDirection.Left)
                ]);
            Position start = new Position(0, 0, Direction.East);
            Position end = repeat.Move(start);
            Assert.Equal(new Position(0, 10, Direction.South), end);
        }
    }
}
