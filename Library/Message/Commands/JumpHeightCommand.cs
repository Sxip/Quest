using Library.Cheat;
using Library.Message.Attributes;

namespace Library.Message.Commands
{
    public static class JumpHeightCommand
    {
        [Command(commandName: "jumpheight", description: "Sets the jump height multiplier.")]
        public static void JumpHeightCommandProcess(string argument)
        {
            if (!float.TryParse(argument, out var height))
            {
                Chat.Instance.AddText("Invalid jump height value. Please enter a valid float.");
                return;
            }
            
            CheatManager.Instance.ToggleCheat("JumpHeightCheat", height);
        }
    }
}