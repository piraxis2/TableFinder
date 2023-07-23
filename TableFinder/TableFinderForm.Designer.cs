using System.Windows.Forms;

namespace TableFinder
{
    partial class TableFinderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableFinderForm));
            this.listBox = new System.Windows.Forms.ListBox();
            this.miniToolStrip = new System.Windows.Forms.ToolStrip();
            this.Finder = new System.Windows.Forms.ToolStripTextBox();
            this.findButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.StopButton = new System.Windows.Forms.ToolStripButton();
            this.PathText = new System.Windows.Forms.ToolStripLabel();
            this.FilePathBox = new System.Windows.Forms.ToolStripLabel();
            this.miniToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.listBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.listBox.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 12;
            this.listBox.Location = new System.Drawing.Point(15, 32);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(773, 388);
            this.listBox.TabIndex = 0;
            this.listBox.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.Finder, this.findButton, this.toolStripProgressBar, this.StopButton, this.PathText, this.FilePathBox});
            this.miniToolStrip.Location = new System.Drawing.Point(0, 0);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.miniToolStrip.Size = new System.Drawing.Size(791, 29);
            this.miniToolStrip.TabIndex = 0;
            this.miniToolStrip.TabStop = true;
            this.miniToolStrip.Text = "miniToolStrip";
            // 
            // Finder
            // 
            this.Finder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Finder.Margin = new System.Windows.Forms.Padding(20, 0, 1, 0);
            this.Finder.Name = "Finder";
            this.Finder.Size = new System.Drawing.Size(100, 29);
            this.Finder.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FinderOnKeyUp);
            // 
            // findButton
            // 
            this.findButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.findButton.Image = ((System.Drawing.Image) (resources.GetObject("findButton.Image")));
            this.findButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(23, 26);
            this.findButton.Text = "toolStripButton1";
            this.findButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 26);
            // 
            // StopButton
            // 
            this.StopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopButton.Image = ((System.Drawing.Image) (resources.GetObject("StopButton.Image")));
            this.StopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(23, 26);
            this.StopButton.Text = "toolStripButton1";
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // PathText
            // 
            this.PathText.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PathText.Margin = new System.Windows.Forms.Padding(100, 1, 0, 2);
            this.PathText.Name = "PathText";
            this.PathText.Size = new System.Drawing.Size(34, 26);
            this.PathText.Text = "경로:";
            // 
            // FilePathBox
            // 
            this.FilePathBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.FilePathBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.FilePathBox.ForeColor = System.Drawing.Color.Coral;
            this.FilePathBox.ImageTransparentColor = System.Drawing.Color.Cyan;
            this.FilePathBox.Name = "FilePathBox";
            this.FilePathBox.Size = new System.Drawing.Size(88, 26);
            this.FilePathBox.Text = "toolStripLabel1";
            this.FilePathBox.Click += new System.EventHandler(this.FilePath_Click);
            // 
            // TableFinderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.miniToolStrip);
            this.Controls.Add(this.listBox);
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "TableFinderForm";
            this.miniToolStrip.ResumeLayout(false);
            this.miniToolStrip.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ToolStripLabel FilePathBox;

        private System.Windows.Forms.ToolStripLabel PathText;

        private System.Windows.Forms.ToolStripButton StopButton;

        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;

        private System.Windows.Forms.ToolStrip miniToolStrip;

        private System.Windows.Forms.ToolStripButton findButton;

        private System.Windows.Forms.ToolStripTextBox Finder;

        private System.Windows.Forms.ListBox listBox;

        #endregion
    }
}