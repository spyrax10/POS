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
    public partial class introForm : Form
    {
        int timeleft;

        public introForm()
        {
            InitializeComponent();
        }

        private void introForm_Load(object sender, EventArgs e)
        {


            timer1.Enabled = true;
            timeleft = 43;
            timer1.Start();

            progressBar1.Maximum = 43;
            timer1.Tick += new EventHandler(timer1_Tick);


        }

        void timer1_Tick(object sender, EventArgs e)
        {

            try
            {
                timeleft--;
                if (progressBar1.Value != 43)
                {
                    progressBar1.Value++;
                    if (timeleft == 0)
                    {
                        timer1.Stop();
                        this.Visible = false;
                        ShowInTaskbar = false;
                        userPassForm pass = new userPassForm();
                        pass.Show();
                    }
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

       
    }
}
