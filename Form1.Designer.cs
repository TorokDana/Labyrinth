
namespace Labyrinth
{
    partial class GameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.újJátékToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._menuGameEasy = new System.Windows.Forms.ToolStripMenuItem();
            this._menuGameMedium = new System.Windows.Forms.ToolStripMenuItem();
            this._menuGameHard = new System.Windows.Forms.ToolStripMenuItem();
            this.szünetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._toolLabelGameTime = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this._toolLabelGameTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.újJátékToolStripMenuItem,
            this.szünetToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(864, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // újJátékToolStripMenuItem
            // 
            this.újJátékToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuGameEasy,
            this._menuGameMedium,
            this._menuGameHard});
            this.újJátékToolStripMenuItem.Name = "újJátékToolStripMenuItem";
            this.újJátékToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.újJátékToolStripMenuItem.Text = "Új játék";
            // 
            // _menuGameEasy
            // 
            this._menuGameEasy.Name = "_menuGameEasy";
            this._menuGameEasy.Size = new System.Drawing.Size(148, 26);
            this._menuGameEasy.Text = "Kicsi";
            this._menuGameEasy.Click += new System.EventHandler(this.kicsiToolStripMenuItem_Click);
            // 
            // _menuGameMedium
            // 
            this._menuGameMedium.Name = "_menuGameMedium";
            this._menuGameMedium.Size = new System.Drawing.Size(148, 26);
            this._menuGameMedium.Text = "Közepes";
            this._menuGameMedium.Click += new System.EventHandler(this.közepesToolStripMenuItem_Click);
            // 
            // _menuGameHard
            // 
            this._menuGameHard.Name = "_menuGameHard";
            this._menuGameHard.Size = new System.Drawing.Size(148, 26);
            this._menuGameHard.Text = "Nagy";
            this._menuGameHard.Click += new System.EventHandler(this.nagyToolStripMenuItem_Click);
            // 
            // szünetToolStripMenuItem
            // 
            this.szünetToolStripMenuItem.Name = "szünetToolStripMenuItem";
            this.szünetToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            this.szünetToolStripMenuItem.Text = "Szünet";
            this.szünetToolStripMenuItem.Click += new System.EventHandler(this.szünetToolStripMenuItem_Click);
            // 
            // _toolLabelGameTime
            // 
            this._toolLabelGameTime.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this._toolLabelGameTime.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._toolLabelGameTime.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this._toolLabelGameTime.Location = new System.Drawing.Point(0, 879);
            this._toolLabelGameTime.Name = "_toolLabelGameTime";
            this._toolLabelGameTime.Size = new System.Drawing.Size(864, 26);
            this._toolLabelGameTime.TabIndex = 1;
            this._toolLabelGameTime.Text = "Idő";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(72, 20);
            this.toolStripStatusLabel1.Text = "Eltelt idő:";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 20);
            // 
            // GameForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(864, 905);
            this.Controls.Add(this._toolLabelGameTime);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimizeBox = false;
            this.Name = "GameForm";
            this.Text = "Labirintus";
            this.Load += new System.EventHandler(this.GameForm_Load_1);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this._toolLabelGameTime.ResumeLayout(false);
            this._toolLabelGameTime.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem újJátékToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _menuGameEasy;
        private System.Windows.Forms.ToolStripMenuItem _menuGameMedium;
        private System.Windows.Forms.ToolStripMenuItem _menuGameHard;
        private System.Windows.Forms.ToolStripMenuItem szünetToolStripMenuItem;
        private System.Windows.Forms.StatusStrip _toolLabelGameTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    }
}

