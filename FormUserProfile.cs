using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;
namespace POS1
{
    public partial class FormUserProfile : Form
    {

        public static string fullname;
        public static string ID;


        public FormUserProfile()
        {
            InitializeComponent();
        }

        private void buttonMain_Click(object sender, EventArgs e)
        {
           

        }

        private void FormUserProfile_Load(object sender, EventArgs e)
        {
            labelDateDash.Text = DateTime.Now.ToLongDateString();
            labelIdNoDash.Text = FormLog .passingId;



            SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");



            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from addEmp2 where EmployeeId = '" + labelIdNoDash.Text  + "'";

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                string First = (dr["Firstname"].ToString());
                string Middle = (dr["Middlename"].ToString());
                string Last = (dr["Lastname"].ToString());
                labelPosDash.Text = (dr["Position"].ToString());
                labelJoinDash.Text = (dr["DateJoin"].ToString());
                labelFullDash.Text = First + " " + Middle + " " + Last;


                byte[] img = (byte[])(dr["Image"]);
                if (img == null)
                {
                    pictureBoxUserDash.Image = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(img);
                    pictureBoxUserDash.Image = Image.FromStream(ms);
                }

            }


        }

        private void buttonPos_Click(object sender, EventArgs e)
        {
            this.Hide();
            ShowInTaskbar = false;
            fullname = labelFullDash.Text;
            ID = labelIdNoDash.Text;
            FormPosMain pos = new FormPosMain();
            pos.Show();
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
    }
}
