public class GetUserInput() {
    public static Action AskQuestion(string question, Dictionary<string, Action> actions, Func<string> onError);
    public static void ReadInput(Dictionary<string, Action> actions, Func<string> onError, string escape);
}