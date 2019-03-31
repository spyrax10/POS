using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Drawing.Imaging;





namespace POS1
{
    public partial class FormAdminDash : Form
    {

        public static string Idpass;


        public FormAdminDash()
        {
            InitializeComponent();
        }
        
        private void buttonUpEmp_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;

            FormEditEmp empEdit = new FormEditEmp();
            empEdit.Show();
        }

        private void buttonViewPro_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;

            FormViewPro viewPro = new FormViewPro();
            viewPro.Show();
        }

        private void buttonAddPro_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;


            FormAddPro add = new FormAddPro();
            add.Show();
        }

        private void buttonNewEmp_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;

            FormAddEmp addEmp = new FormAddEmp();
            addEmp.Show();
        }

        private void buttonViewEmp_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;


            FormViewEmp viewEmp = new FormViewEmp();
            viewEmp.Show();
        }

        private void buttonUpPro_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;

            FormEditPro editPro = new FormEditPro();
            editPro.Show();
        }

        private void buttonOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLog log = new FormLog ();
            log.Show();
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void FormAdminDash_Load(object sender, EventArgs e)
        {
            timer1.Start();
            labelDateAdmin.Text = DateTime.Now.ToLongDateString();
            labelAdminTime.Text = DateTime.Now.ToLongTimeString();
            labelDashId.Text = FormLog .passingId;
            buttonAddPro.Focus();

            SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");



            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from addEmp2 where EmployeeId = '" + labelDashId .Text + "'";

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
               string first  = (dr["Firstname"].ToString());

                labelAdminName.Text = first + "" + "!";

                byte[] img = (byte[])(dr["Image"]);
                if (img == null)
                {
                    pictureBoxAdminDash .Image = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(img);
                    pictureBoxAdminDash .Image = Image.FromStream(ms);
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelAdminTime.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;


            FormInvoice sales = new FormInvoice();
            sales.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;
            Idpass = labelDashId.Text;
            FormPosMain pos = new FormPosMain();
            pos.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            this.Visible = false;
            ShowInTaskbar = false;

            returnItem item = new returnItem();
            item.Show();
        }
    }
}
