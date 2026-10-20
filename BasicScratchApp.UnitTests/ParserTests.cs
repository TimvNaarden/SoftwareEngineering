namespace BasicScratchApp.UnitTests
{
    public class ParserTests
    {
        [Fact]
        public void TestInvalidInput()
        {
            Assert.Throws<KeyNotFoundException>(() => { Parser.ParseFile("Invalid.textfiletester"); });
        }

        [Fact]
        public void TestBasicExample()
        {
            Assert.Equal(Examples.BasicProgram.ToString(), Parser.ParseFile("../../../Examples/Basic.txt").ToString());
        }

        [Fact]
        public void TestAdvancedExample()
        {
            Assert.Equal(Examples.AdvancedProgram.ToString(), Parser.ParseFile("../../../Examples/Advanced.txt").ToString());
        }

        [Fact]
        public void TestExpertExample()
        {
            Assert.Equal(Examples.ExpertProgram.ToString(), Parser.ParseFile("../../../Examples/Expert.txt").ToString());
        }
    }
}
