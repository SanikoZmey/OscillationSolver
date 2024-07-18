namespace MyNNSolver._2nd_order_equation
{
    partial class FormSolution
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_YToV = new System.Windows.Forms.Panel();
            this.panel_VToT = new System.Windows.Forms.Panel();
            this.panel_YtoT = new System.Windows.Forms.Panel();
            this.elementHostUCBall = new System.Windows.Forms.Integration.ElementHost();
            this.uC_ball1 = new MyNNSolver._2nd_order_equation.UC_ball();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel_YToV, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel_VToT, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel_YtoT, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.elementHostUCBall, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel_YToV
            // 
            this.panel_YToV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_YToV.Location = new System.Drawing.Point(403, 228);
            this.panel_YToV.Name = "panel_YToV";
            this.panel_YToV.Size = new System.Drawing.Size(394, 219);
            this.panel_YToV.TabIndex = 2;
            // 
            // panel_VToT
            // 
            this.panel_VToT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_VToT.Location = new System.Drawing.Point(3, 228);
            this.panel_VToT.Name = "panel_VToT";
            this.panel_VToT.Size = new System.Drawing.Size(394, 219);
            this.panel_VToT.TabIndex = 1;
            // 
            // panel_YtoT
            // 
            this.panel_YtoT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_YtoT.Location = new System.Drawing.Point(3, 3);
            this.panel_YtoT.Name = "panel_YtoT";
            this.panel_YtoT.Size = new System.Drawing.Size(394, 219);
            this.panel_YtoT.TabIndex = 0;
            // 
            // elementHostUCBall
            // 
            this.elementHostUCBall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHostUCBall.Location = new System.Drawing.Point(403, 3);
            this.elementHostUCBall.Name = "elementHostUCBall";
            this.elementHostUCBall.Size = new System.Drawing.Size(394, 219);
            this.elementHostUCBall.TabIndex = 3;
            this.elementHostUCBall.Text = "elementHost1";
            this.elementHostUCBall.Child = this.uC_ball1;
            // 
            // FormSolution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormSolution";
            this.Text = "FormSolution";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel_YToV;
        private System.Windows.Forms.Panel panel_VToT;
        private System.Windows.Forms.Panel panel_YtoT;
        private System.Windows.Forms.Integration.ElementHost elementHostUCBall;
        private UC_ball uC_ball1;
    }
}