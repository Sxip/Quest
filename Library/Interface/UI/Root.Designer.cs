namespace Library.Interface.UI
{
    partial class Root
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.pnlGame = new System.Windows.Forms.Panel();
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.githubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGame
            // 
            this.pnlGame.AutoSize = true;
            this.pnlGame.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlGame.BackColor = System.Drawing.SystemColors.Control;
            this.pnlGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGame.Location = new System.Drawing.Point(0, 0);
            this.pnlGame.Name = "pnlGame";
            this.pnlGame.Size = new System.Drawing.Size(790, 450);
            this.pnlGame.TabIndex = 4;
            // 
            // Menu
            // 
            this.Menu.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem;
            this.Menu.BackColor = System.Drawing.SystemColors.Control;
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.fileToolStripMenuItem });
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(790, 24);
            this.Menu.TabIndex = 5;
            this.Menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.pluginsToolStripMenuItem, this.githubToolStripMenuItem, this.unloadToolStripMenuItem });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.fileToolStripMenuItem.Text = "Menu";
            // 
            // pluginsToolStripMenuItem
            // 
            this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
            this.pluginsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.pluginsToolStripMenuItem.Text = "Plugins Directory";
            this.pluginsToolStripMenuItem.Click += new System.EventHandler(this.pluginsToolStripMenuItem_Click);
            // 
            // githubToolStripMenuItem
            // 
            this.githubToolStripMenuItem.Name = "githubToolStripMenuItem";
            this.githubToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.githubToolStripMenuItem.Text = "Github";
            this.githubToolStripMenuItem.Click += new System.EventHandler(this.githubToolStripMenuItem_Click);
            // 
            // unloadToolStripMenuItem
            // 
            this.unloadToolStripMenuItem.Name = "unloadToolStripMenuItem";
            this.unloadToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.unloadToolStripMenuItem.Text = "Unload";
            this.unloadToolStripMenuItem.Click += new System.EventHandler(this.unloadToolStripMenuItem_Click);
            // 
            // Root
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 450);
            this.Controls.Add(this.Menu);
            this.Controls.Add(this.pnlGame);
            this.MainMenuStrip = this.Menu;
            this.Name = "Root";
            this.Text = "Quest";
            this.Load += new System.EventHandler(this.Root_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Root_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Root_KeyUp);
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStripMenuItem pluginsToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem githubToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unloadToolStripMenuItem;

        private System.Windows.Forms.MenuStrip Menu;

        #endregion

        public System.Windows.Forms.Panel pnlGame;
    }
}