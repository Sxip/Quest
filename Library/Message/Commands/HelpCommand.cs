using Library.Message.Attributes;

namespace Library.Message.Commands
{
    public class HelpCommand
    {
        [Command(commandName: "help", description: "Shows a list of available commands.")]
        public static void HelpCommandProcess(string argument)
        {
            Chat.Instance.AddText("Available commands:");
            
            foreach (var command in MessageManager.Instance.Commands)
                Chat.Notify($"!{command.Key}");
        }
    }
}