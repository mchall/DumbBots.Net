using System;
using System.Windows.Forms;

namespace DumbBots.NET
{
    /// <summary>
    /// Show information about the project
    /// </summary>
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}