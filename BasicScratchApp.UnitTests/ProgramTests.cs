using System.Runtime.CompilerServices;

namespace BasicScratchApp.UnitTests
{
    // Tests can be ran using 'dotnet test BasicScratchApp.UnitTests'
    // Any file in this folder will be ran
    // It can import any class/method from BasicScratchApp, that are public. 
    public class ProgramTests
    {
        // This test runs one time 
        [Fact]
        public void BasicProgram()
        {
            Assert.Equal(1, 1);
        }

        //This test runs for every [InlineData(x,y,z)] entry once        
        [Theory]
        [InlineData(1, 2, 3)] // BasicTheoryTest(1,2,3)
        [InlineData(4, 1, 5)] // BasicTheoryTest(1,4,5)
        public void BasicTheoryTest(int arg1, int arg2, int arg3)
        {
            Assert.Equal(arg1 + arg2, arg3);
            Assert.InRange(arg2, arg1, arg3); // fails on second entry. 1 is not in range 4-5
        }
    }
}
