using Library.Message.Attributes;

namespace Library.Message.Commands
{
    public static class BankCommand
    {
        [Command(commandName: "bank", description: "Opens the bank menu")]
        public static void BankCommandProcess(string argument) => UIBankManager.Show();     
    }
}