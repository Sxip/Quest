using Library.Message.Attributes;

namespace Library.Message.Commands
{
    public static class ShopCommand
    {
        [Command(commandName: "shop", description: "Opens the shop menu")]
        public static void ShopCommandProcess(string argument)
        {
            if (!int.TryParse(argument, out var shop))
            {
                Chat.Instance.AddText("Invalid shop value. Please enter a valid integer.");
                return;
            }

            UIShop.LoadShop(shop);
        }
    }
}