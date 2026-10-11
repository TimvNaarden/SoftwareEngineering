namespace BasicScratchApp
{
    public class EntryPoint()
    {
        // Dictionary<string, Func<Program>> instead of Dictionary<string, Program> since the input function needs to be run
        readonly static Dictionary<string, Func<Program>> exampleProgramSelections = new()
        {
            { "Basic",  () => Examples.BasicProgram},
            { "0",  () => Examples.BasicProgram},

            { "Advanced",  () => Examples.AdvancedProgram},
            { "1",  () => Examples.AdvancedProgram},

            { "Expert",  () => Examples.ExportProgram},
            { "2",  () => Examples.ExportProgram},

            { "Input File",  () => Parser.ParseFile(UserInput.AskQuestion("Enter the path to the input file: "))},
            { "3",  () => Parser.ParseFile(UserInput.AskQuestion("Enter the path to the input file: "))}
        };
        readonly static string exampleProgramSelectionPrompt = "Select an program to run (Basic(0), Advanced(1), Expert(2), Input File(3)): ";
        readonly static string metricOrRun = "Do you want to run the program(0) or print the metrics(1)? ";
        private static void Main(string[] args)
        {
            Program selected = UserInput.AskQuestion(exampleProgramSelectionPrompt, exampleProgramSelections, () => Console.WriteLine("Input not recognized, selecting Basic"))();

            // Computed here with selected option
            Dictionary<string, Action> extractUserAction = new()
            {
                {"0", () =>  Console.WriteLine(selected.ToString())},
                {"1", () => Console.WriteLine(selected.Statistics())}
            };

            Action extractedAction = UserInput.AskQuestion(metricOrRun, extractUserAction, () => Console.WriteLine("Input not recognized, running the program"));

            extractedAction();
        }
    }
}