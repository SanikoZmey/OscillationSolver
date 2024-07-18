using MyNNSolver._2nd_order_equation;
using System;
using System.Windows.Forms;

namespace MyNNSolver
{
    public partial class Form_Main : Form
    {
        private Form_Parameters form_Parameters;
        public Form_Main()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void secondOrderEquationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Form_Parameters"] == null)
            {
                form_Parameters = new Form_Parameters { MdiParent = this };
                form_Parameters.Show();
            }
            else
            {
                form_Parameters.BringToFront();
            }
        }
    }
}
