using Library.Interface.Attributes;
using Library.PacketSpammer.UI;

namespace Library.PacketSpammer
{
    [InterfaceMenuItem(name: "Packet Spammer")]
    public class PacketSpammerMenuItem
    {
        [MenuItemClicked]
        public void Show()
        {
            
        }
    }
}