namespace MyNNSolver
{
    partial class Form_Main
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
            this.menuStrip_ProblemActions = new System.Windows.Forms.MenuStrip();
            this.problemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secondOrderEquationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_ProblemActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip_ProblemActions
            // 
            this.menuStrip_ProblemActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.problemToolStripMenuItem});
            this.menuStrip_ProblemActions.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_ProblemActions.Name = "menuStrip_ProblemActions";
            this.menuStrip_ProblemActions.Size = new System.Drawing.Size(784, 24);
            this.menuStrip_ProblemActions.TabIndex = 0;
            // 
            // problemToolStripMenuItem
            // 
            this.problemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.problemToolStripMenuItem.Name = "problemToolStripMenuItem";
            this.problemToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.problemToolStripMenuItem.Text = "Problem";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.secondOrderEquationToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // secondOrderEquationToolStripMenuItem
            // 
            this.secondOrderEquationToolStripMenuItem.Name = "secondOrderEquationToolStripMenuItem";
            this.secondOrderEquationToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.secondOrderEquationToolStripMenuItem.Text = "Second order equation";
            this.secondOrderEquationToolStripMenuItem.Click += new System.EventHandler(this.secondOrderEquationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(95, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.menuStrip_ProblemActions);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip_ProblemActions;
            this.Name = "Form_Main";
            this.Text = "NNSolver";
            this.menuStrip_ProblemActions.ResumeLayout(false);
            this.menuStrip_ProblemActions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip_ProblemActions;
        private System.Windows.Forms.ToolStripMenuItem problemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem secondOrderEquationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}