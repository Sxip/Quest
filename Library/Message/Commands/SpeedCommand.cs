using Library.Game.Objects.Player;
using Library.Message.Attributes;

namespace Library.Message.Commands
{
    public static class SpeedCommand
    {
        [Command(commandName: "speed", description: "Sets the speed of the current player.")]
        public static void SpeedCommandProcess(string argument)
        {
            if (!int.TryParse(argument, out var speed))
            {
                Chat.Instance.AddText("Invalid speed value. Please enter a valid integer.");
                return;
            }

            // Base stats 10 is the speed of the player
            PlayerObject.Me.statsCurrent[10] = speed;
        }
    }
}