using System;
using System.Drawing;
using System.Windows.Forms;

namespace Library.PacketSniffer.UI
{
    public partial class PacketSnifferInterface : Form
    {
        /// <summary>
        /// Instance of this interface.
        /// </summary>
        public static readonly PacketSnifferInterface Instance = new PacketSnifferInterface();
        
        public PacketSnifferInterface()
        {
            InitializeComponent();
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            PacketSniffer.PacketReceived += OnPacketReceived;
            PacketSniffer.PacketSent += OnPacketSent;
            
            PacketSniffer.Start();
            StartBtn.Enabled = false;
        }
        
        public void AddText(string text)
        {
            TxtPackets.Invoke((MethodInvoker)delegate
            {
                TxtPackets.AppendText($"[Received] {text} {System.Environment.NewLine}");
            });
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
             PacketSniffer.PacketReceived -= OnPacketReceived;
             PacketSniffer.PacketSent -= OnPacketSent;
             
             PacketSniffer.Stop();
             StartBtn.Enabled = true;
        }
        
        private void OnPacketReceived(string packet)
        {
            TxtPackets.Invoke((MethodInvoker)delegate
            {
                TxtPackets.SelectionColor = Color.Green;
                TxtPackets.AppendText($"[Received] {packet} {System.Environment.NewLine}");
            });
        }

        private void OnPacketSent(string packet)
        {
            TxtPackets.Invoke((MethodInvoker)delegate
            {
                
                TxtPackets.SelectionColor = Color.Red;
                TxtPackets.AppendText($"[Sent] {packet} {System.Environment.NewLine}");
            });
        }

        private void PacketSnifferInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) return;
            
            e.Cancel = true;
            Hide();
        }
    }
}