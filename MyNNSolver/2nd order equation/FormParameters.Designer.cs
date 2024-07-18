namespace MyNNSolver._2nd_order_equation
{
    partial class Form_Parameters
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
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Continue = new System.Windows.Forms.Button();
            this.propertyGrid_Parameters = new System.Windows.Forms.PropertyGrid();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.button_Cancel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_Continue, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.propertyGrid_Parameters, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(349, 491);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button_Cancel
            // 
            this.button_Cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_Cancel.Location = new System.Drawing.Point(3, 459);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(168, 29);
            this.button_Cancel.TabIndex = 0;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // button_Continue
            // 
            this.button_Continue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_Continue.Location = new System.Drawing.Point(177, 459);
            this.button_Continue.Name = "button_Continue";
            this.button_Continue.Size = new System.Drawing.Size(169, 29);
            this.button_Continue.TabIndex = 1;
            this.button_Continue.Text = "Continue";
            this.button_Continue.UseVisualStyleBackColor = true;
            this.button_Continue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // propertyGrid_Parameters
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.propertyGrid_Parameters, 2);
            this.propertyGrid_Parameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid_Parameters.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid_Parameters.Name = "propertyGrid_Parameters";
            this.propertyGrid_Parameters.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid_Parameters.Size = new System.Drawing.Size(343, 450);
            this.propertyGrid_Parameters.TabIndex = 2;
            // 
            // Form_Parameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 491);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(0, 1);
            this.Name = "Form_Parameters";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Parameters";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Continue;
        private System.Windows.Forms.PropertyGrid propertyGrid_Parameters;
    }
}