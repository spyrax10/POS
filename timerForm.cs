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
    public partial class timerForm : Form
    {

        int timeleft;

        public timerForm()
        {
            InitializeComponent();
        }

        private void timerForm_Load(object sender, EventArgs e)
        {
            button1.Visible = false;


            timeleft = 15;
            label2.Text = "15seconds";
            timer1.Start();



        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            timeleft--;
            label2.Text = timeleft + " seconds";


            if(timeleft == 0)
            {
                timer1.Stop();
                label2.Visible = false;
                button1.Visible = true;
                label1.Visible = false;

            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;

            FormLog  log = new FormLog ();
            log.Show();

        }
    }
}
