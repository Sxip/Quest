using System.ComponentModel;

namespace Library.PacketSniffer.UI
{
    partial class PacketSnifferInterface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PacketSnifferInterface));
            this.TxtPackets = new System.Windows.Forms.RichTextBox();
            this.StartBtn = new System.Windows.Forms.Button();
            this.StopBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxtPackets
            // 
            this.TxtPackets.Location = new System.Drawing.Point(12, 57);
            this.TxtPackets.Name = "TxtPackets";
            this.TxtPackets.Size = new System.Drawing.Size(586, 344);
            this.TxtPackets.TabIndex = 0;
            this.TxtPackets.Text = "";
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(12, 12);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(75, 23);
            this.StartBtn.TabIndex = 1;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // StopBtn
            // 
            this.StopBtn.Location = new System.Drawing.Point(93, 12);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(75, 23);
            this.StopBtn.TabIndex = 2;
            this.StopBtn.Text = "Stop";
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // PacketSnifferInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 413);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.TxtPackets);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PacketSnifferInterface";
            this.Text = "Packet Sniffer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PacketSnifferInterface_FormClosing);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.RichTextBox TxtPackets;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Button StopBtn;

        #endregion
    }
}