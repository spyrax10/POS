using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS1
{
    public partial class viewReceipt : Form
    {
        public viewReceipt()
        {
            InitializeComponent();
        }

        private void viewReceipt_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = FormPosMain.receipt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;
        }
    }
}
