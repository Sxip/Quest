using Library.Cheat;
using Library.Message.Attributes;

namespace Library.Message.Commands
{
    public static class FlyCommand
    {   
        [Command(commandName: "fly", description: "Toggles player fly mode")]
        public static void FlyCommandProcess(string argument) => CheatManager.Instance.ToggleCheat("FlyCheat");
    }
}