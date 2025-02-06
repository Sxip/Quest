using Library.Interface.Attributes;
using Library.PacketSniffer.UI;

namespace Library.PacketSniffer
{
    [InterfaceMenuItem(name: "Packet Sniffer")]
    public class PacketSnifferMenuItem
    {
        [MenuItemClicked]
        public void Show()
        {
            if (!PacketSnifferInterface.Instance.Visible) PacketSnifferInterface.Instance.Show();
        }
    }
}