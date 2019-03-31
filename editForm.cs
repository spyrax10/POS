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



namespace POS1
{
    public partial class editForm : Form
    {
        public editForm()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

        private void editForm_Load(object sender, EventArgs e)
        {
            labelEditEmpId.Text = FormEditEmp.ID;


            string first = FormEditEmp.first;
            string mid = FormEditEmp.middle;
            string last = FormEditEmp.last;

            labelEditFullname .Text = first + " " + mid + " " + last;


            SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from authenTab where EmployeeId = '" + labelEditEmpId .Text + "'";

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                textBoxEditUser.Text = (dr["Username"].ToString());
                textBoxEditPass.Text = (dr["Password"].ToString());
                textBoxEditReType.Text=(dr["Password"].ToString());
                comboBoxEditSecQuest.Text = (dr["SecurrityQuestion"].ToString());
                textBoxEditAns.Text = (dr["Answer"].ToString());

            }

         }

        private void buttonEditCont_Click(object sender, EventArgs e)
        {


            if (textBoxEditPass.Text != textBoxEditReType.Text)
            {
                MessageBox.Show("Password don't Match");
                textBoxEditPass.Text = string.Empty;
                textBoxEditReType.Text = string.Empty;

            }
            else if (textBoxEditUser.Text == "" || textBoxEditReType.Text == "" || textBoxEditPass.Text == "" || comboBoxEditSecQuest.Text == "" || textBoxEditAns.Text == "")
            {
                MessageBox.Show("Please fill the empty fields");
            }
            else
            {
               
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE authenTab SET Username = '" + textBoxEditUser.Text + "', Password = '" + textBoxEditPass.Text + "', SecurrityQuestion = '" + comboBoxEditSecQuest.Text + "', Answer = '" + textBoxEditAns.Text + "' WHERE EmployeeID = '" + labelEditEmpId.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Done....");
                this.Visible = false;
                ShowInTaskbar = false;

                FormAdminDash add = new FormAdminDash();
                add.Show();


            }
           
        }
    }
}
