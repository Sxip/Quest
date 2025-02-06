using Library.Interface.Attributes;
using Library.Plugins.UI;

namespace Library.Plugins
{
    [InterfaceMenuItem(name: "Plugins")]
    public class PluginManagerMenuItem
    {
        [MenuItemClicked]
        public void Show()
        {
            if (!PluginManagerInterface.Instance.Visible) PluginManagerInterface.Instance.Show();
        }
    }
}