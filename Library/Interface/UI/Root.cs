using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Library.Window;

namespace Library.Interface.UI
{
    public partial class Root : Form
    {
        public static readonly Root Instance = new Root();

        private Root()
        {
            InitializeComponent();
            
            Resize += Root_Resize;
            Menu.Parent = this;
            Menu.Visible = false;
        }
        
        private void Root_Load(object sender, EventArgs e)
        {
            AdaptGameWindow();

            try
            {
                InterfaceManager.Instance.RegisterAllMenuItems();
            }
            catch (Exception exception)
            {
                File.AppendAllText($"{AppDomain.CurrentDomain.BaseDirectory}/exception.log", $"{exception.Message}\n");
            }
        }
        
        private void Root_Resize(object sender, EventArgs e)
        {
            if (WindowGameAttachment.GameWindowAttached)
            {
                WindowGameAttachment.Resize(ref pnlGame);
            }
        }

        public ToolStripMenuItem FindMenuItem(string name)
        {
            return Menu.Items.OfType<ToolStripMenuItem>().FirstOrDefault(item => item.Text == name);
        }
        
        public void AddMenuItem(ToolStripMenuItem item)
        {
            Menu.Items.Add(item);
        }
        
        private void AdaptGameWindow()
        {
            WindowGameAttachment.GameWindowHandle = WindowGameAttachment.FindWindow(null, "AQ3D");
            if (WindowGameAttachment.GameWindowHandle == IntPtr.Zero)
            {
                MessageBox.Show("Game Window Not Found", "Error");
            }
            
            WindowGameAttachment.OriginalWindowStyle = WindowGameAttachment.GetWindowLong(WindowGameAttachment.GameWindowHandle, WindowGameAttachment.GwlStyle);
            WindowGameAttachment.GetWindowRect(WindowGameAttachment.GameWindowHandle, out WindowGameAttachment.OriginalWindowPos);
            WindowGameAttachment.SetWindowLong(WindowGameAttachment.GameWindowHandle, WindowGameAttachment.GwlStyle, WindowGameAttachment.WsVisible);
            WindowGameAttachment.SetParent(WindowGameAttachment.GameWindowHandle, pnlGame.Handle);
            WindowGameAttachment.MoveWindow(WindowGameAttachment.GameWindowHandle, 0, 0, pnlGame.Width, pnlGame.Height, repaint: true);
            WindowGameAttachment.GameWindowAttached = true;
        }
        
        public void ResetUi()
        {
            WindowGameAttachment.SetParent(WindowGameAttachment.GameWindowHandle, IntPtr.Zero);
            WindowGameAttachment.SetWindowLong(WindowGameAttachment.GameWindowHandle, WindowGameAttachment.GwlStyle, (int)WindowGameAttachment.OriginalWindowStyle);
            WindowGameAttachment.MoveWindow(WindowGameAttachment.GameWindowHandle, WindowGameAttachment.OriginalWindowPos.left, WindowGameAttachment.OriginalWindowPos.top, WindowGameAttachment.OriginalWindowPos.right, WindowGameAttachment.OriginalWindowPos.bottom, repaint: true);
        }

        private void Root_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                Menu.Visible = true;
            }
        }

        private void Root_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                Menu.Visible = false;
            }
        }

        private void githubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string url = "https://github.com/sxip/quest";
            Process.Start(url);
        }

        private void pluginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var directory = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Quest"));
            Process.Start(directory.FullName);
        }

        private void unloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quest.Loader.Unload();
        }
    }
}
