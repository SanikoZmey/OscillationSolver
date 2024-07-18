using System;
using System.Windows.Forms;

namespace MyNNSolver._2nd_order_equation
{
    public partial class Form_Parameters : Form
    {
        private Form_Training formTraining;
        private Parameter parameter;
        public Form_Parameters()
        {
            InitializeComponent();

            parameter = new Parameter();
            propertyGrid_Parameters.SelectedObject = parameter;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["Form_Training"] == null)
            {
                formTraining = new Form_Training(parameter) { MdiParent = MdiParent };
                formTraining.Show();
            }
            else
            {
                formTraining.BringToFront();
            }
        }
    }
}
