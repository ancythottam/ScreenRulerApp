namespace ScreenRulerApp
{
    partial class ScreenRulerFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenRulerFrm));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemFlip = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPixel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemInch = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCentimeter = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.AllowDrop = true;
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFlip,
            this.menuItemPixel,
            this.menuItemInch,
            this.menuItemCentimeter,
            this.menuItemExit});
            this.contextMenuStrip.Name = "contextMenu";
            this.contextMenuStrip.ShowCheckMargin = true;
            this.contextMenuStrip.ShowImageMargin = false;
            this.contextMenuStrip.Size = new System.Drawing.Size(153, 136);
            // 
            // menuItemFlip
            // 
            this.menuItemFlip.Name = "menuItemFlip";
            this.menuItemFlip.Size = new System.Drawing.Size(152, 22);
            this.menuItemFlip.Text = "FlipRuler";
            this.menuItemFlip.Click += new System.EventHandler(this.OnMenuItemFlipClick);
            // 
            // menuItemPixel
            // 
            this.menuItemPixel.BackColor = System.Drawing.SystemColors.Control;
            this.menuItemPixel.Checked = true;
            this.menuItemPixel.CheckOnClick = true;
            this.menuItemPixel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemPixel.Name = "menuItemPixel";
            this.menuItemPixel.Size = new System.Drawing.Size(152, 22);
            this.menuItemPixel.Text = "Pixels";
            this.menuItemPixel.Click += new System.EventHandler(this.OnMenuItemPixel_Click);
            // 
            // menuItemInch
            // 
            this.menuItemInch.Name = "menuItemInch";
            this.menuItemInch.Size = new System.Drawing.Size(152, 22);
            this.menuItemInch.Text = "Inches";
            this.menuItemInch.Click += new System.EventHandler(this.OnMenuItemInch_Click);
            // 
            // menuItemCentimeter
            // 
            this.menuItemCentimeter.Name = "menuItemCentimeter";
            this.menuItemCentimeter.Size = new System.Drawing.Size(152, 22);
            this.menuItemCentimeter.Text = "Centimeters";
            this.menuItemCentimeter.Click += new System.EventHandler(this.OnMenuItemCentimeter_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(152, 22);
            this.menuItemExit.Text = "Exit Ruler";
            this.menuItemExit.Click += new System.EventHandler(this.OnMenuItemExit_Click);
            // 
            // ScreenRulerFrm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Gold;
            this.ClientSize = new System.Drawing.Size(400, 45);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(45, 45);
            this.Name = "ScreenRulerFrm";
            this.Text = "ScreenRulerFrm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OnRuler_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnRuler_Paint);
            this.DoubleClick += new System.EventHandler(this.OnMenuItemFlipClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnRuler_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnRuler_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnRuler_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnRuler_MouseUp);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItemFlip;
        private System.Windows.Forms.ToolStripMenuItem menuItemInch;
        private System.Windows.Forms.ToolStripMenuItem menuItemCentimeter;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        protected System.Windows.Forms.ToolStripMenuItem menuItemPixel;
    }
}

