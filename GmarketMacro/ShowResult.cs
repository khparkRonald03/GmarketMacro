using System;
using System.Windows.Forms;

namespace GmarketMacro
{
    public partial class ShowResult : Form
    {
        public ShowResult(string msg = "")
        {
            InitializeComponent();
            BtnResult.Text = msg;
        }

        private void BtnResult_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
