namespace AutoPlayer
{
    partial class mainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.חדשToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.פרק_הבאToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.פרקקודםToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.יציאהToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axWindowsMediaPlayer = new AutoPlayer.axMediaPlayer();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.חדשToolStripMenuItem,
            this.פרק_הבאToolStripMenuItem,
            this.פרקקודםToolStripMenuItem,
            this.יציאהToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.contextMenuStrip.Size = new System.Drawing.Size(164, 92);
            // 
            // חדשToolStripMenuItem
            // 
            this.חדשToolStripMenuItem.Name = "חדשToolStripMenuItem";
            this.חדשToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.חדשToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.חדשToolStripMenuItem.Text = "Open Media";
            this.חדשToolStripMenuItem.Click += new System.EventHandler(this.חדשToolStripMenuItem_Click);
            // 
            // פרק_הבאToolStripMenuItem
            // 
            this.פרק_הבאToolStripMenuItem.Name = "פרק_הבאToolStripMenuItem";
            this.פרק_הבאToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.פרק_הבאToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.פרק_הבאToolStripMenuItem.Text = "Next Eposide";
            this.פרק_הבאToolStripMenuItem.Click += new System.EventHandler(this.פרק_הבאToolStripMenuItem_Click);
            // 
            // פרקקודםToolStripMenuItem
            // 
            this.פרקקודםToolStripMenuItem.Name = "פרקקודםToolStripMenuItem";
            this.פרקקודםToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.פרקקודםToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.פרקקודםToolStripMenuItem.Text = "Previous Eposide";
            this.פרקקודםToolStripMenuItem.Click += new System.EventHandler(this.פרקקודםToolStripMenuItem_Click);
            // 
            // יציאהToolStripMenuItem
            // 
            this.יציאהToolStripMenuItem.Name = "יציאהToolStripMenuItem";
            this.יציאהToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.יציאהToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.יציאהToolStripMenuItem.Text = "Exit";
            this.יציאהToolStripMenuItem.Click += new System.EventHandler(this.יציאהToolStripMenuItem_Click);
            // 
            // axWindowsMediaPlayer
            // 
            this.axWindowsMediaPlayer.AllowDrop = true;
            this.axWindowsMediaPlayer.ContextMenuStrip = this.contextMenuStrip;
            this.axWindowsMediaPlayer.CustomContextMenu = null;
            this.axWindowsMediaPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axWindowsMediaPlayer.Enabled = true;
            this.axWindowsMediaPlayer.Location = new System.Drawing.Point(0, 0);
            this.axWindowsMediaPlayer.Name = "axWindowsMediaPlayer";
            this.axWindowsMediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer.OcxState")));
            this.axWindowsMediaPlayer.Size = new System.Drawing.Size(996, 480);
            this.axWindowsMediaPlayer.TabIndex = 0;
            this.axWindowsMediaPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.axWindowsMediaPlayer_PlayStateChange);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 480);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.axWindowsMediaPlayer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "mainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private axMediaPlayer axWindowsMediaPlayer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem פרק_הבאToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem פרקקודםToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem יציאהToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem חדשToolStripMenuItem;
    }
}

