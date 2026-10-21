
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

    }
}
