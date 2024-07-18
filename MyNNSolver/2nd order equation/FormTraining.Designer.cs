namespace MyNNSolver._2nd_order_equation
{
    partial class Form_Training
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
            this.button_Terminate = new System.Windows.Forms.Button();
            this.button_Continue = new System.Windows.Forms.Button();
            this.progressBarTraining = new System.Windows.Forms.ProgressBar();
            this.richTB_TrainingLogs = new System.Windows.Forms.RichTextBox();
            this.panelLossEpoch = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.button_Terminate, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.button_Continue, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.progressBarTraining, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.richTB_TrainingLogs, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelLossEpoch, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.15682F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.68024F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(399, 491);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button_Terminate
            // 
            this.button_Terminate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_Terminate.Location = new System.Drawing.Point(3, 444);
            this.button_Terminate.Name = "button_Terminate";
            this.button_Terminate.Size = new System.Drawing.Size(193, 44);
            this.button_Terminate.TabIndex = 0;
            this.button_Terminate.Text = "Terminate";
            this.button_Terminate.UseVisualStyleBackColor = true;
            this.button_Terminate.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // button_Continue
            // 
            this.button_Continue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_Continue.Location = new System.Drawing.Point(202, 444);
            this.button_Continue.Name = "button_Continue";
            this.button_Continue.Size = new System.Drawing.Size(194, 44);
            this.button_Continue.TabIndex = 1;
            this.button_Continue.Text = "Continue";
            this.button_Continue.UseVisualStyleBackColor = true;
            this.button_Continue.Click += new System.EventHandler(this.buttonContunue_Click);
            // 
            // progressBarTraining
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.progressBarTraining, 2);
            this.progressBarTraining.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBarTraining.Location = new System.Drawing.Point(3, 395);
            this.progressBarTraining.Name = "progressBarTraining";
            this.progressBarTraining.Size = new System.Drawing.Size(393, 43);
            this.progressBarTraining.Step = 1;
            this.progressBarTraining.TabIndex = 2;
            // 
            // richTB_TrainingLogs
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.richTB_TrainingLogs, 2);
            this.richTB_TrainingLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTB_TrainingLogs.Location = new System.Drawing.Point(3, 264);
            this.richTB_TrainingLogs.Name = "richTB_TrainingLogs";
            this.richTB_TrainingLogs.Size = new System.Drawing.Size(393, 125);
            this.richTB_TrainingLogs.TabIndex = 3;
            this.richTB_TrainingLogs.Text = "";
            // 
            // panelLossEpoch
            // 
            this.panelLossEpoch.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tableLayoutPanel1.SetColumnSpan(this.panelLossEpoch, 2);
            this.panelLossEpoch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLossEpoch.Location = new System.Drawing.Point(3, 3);
            this.panelLossEpoch.Name = "panelLossEpoch";
            this.panelLossEpoch.Size = new System.Drawing.Size(393, 255);
            this.panelLossEpoch.TabIndex = 4;
            // 
            // Form_Training
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(399, 491);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(365, 1);
            this.Name = "Form_Training";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Training";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button_Terminate;
        private System.Windows.Forms.Button button_Continue;
        private System.Windows.Forms.ProgressBar progressBarTraining;
        private System.Windows.Forms.RichTextBox richTB_TrainingLogs;
        private System.Windows.Forms.Panel panelLossEpoch;
    }
}