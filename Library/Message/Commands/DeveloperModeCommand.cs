using Library.Message.Attributes;

namespace Library.Message.Commands
{
    public class DeveloperModeCommand
    {
        [Command(commandName: "developer", description: "Toggles developer mode.")]
        public static void DeveloperModeCommandProcess(string argument)
        {
            global::Game.Instance.DeveloperModeToggle();
        }
    }
}