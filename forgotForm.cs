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
    public partial class forgotForm : Form
    {
        public forgotForm()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");
        string cs = @"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True";

        private void forgotForm_Load(object sender, EventArgs e)
        {

            label4.Visible = false;
            label5.Visible = false;


            labelForPass.Visible = false;
            labelForUser.Visible = false;



            buttonForLog.Visible = false;

            labelEmploy.Text = FormLog.passingId;


            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select SecurrityQuestion from authenTab where EmployeeId = '" + labelEmploy.Text  + "'";

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {


                labelQuest.Text = (dr["SecurrityQuestion"].ToString());


            }

        }
        private void buttonForVer_Click(object sender, EventArgs e)
        {
           

          






         }

        private void buttonForSubmit_Click(object sender, EventArgs e)
        {

            if (textBoxForAns.Text == "")
            {
                MessageBox.Show("Input your answer....");
            }

            else
            {

                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("Select * from authenTab where EmployeeId = @EmployeeId and Answer = @Answer", con);
                cmd.Parameters.AddWithValue("@EmployeeId", labelEmploy  .Text );
                cmd.Parameters.AddWithValue("@Answer", textBoxForAns.Text);
                con.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                con.Close();


                int count = ds.Tables[0].Rows.Count;
                if (count == 1)
                {

                    con.Open();
                    cmd.CommandText = "Select * from authenTab where EmployeeId= '" + labelEmploy .Text + "'";

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        labelForPass .Text  = (dr["Password"].ToString());
                        labelForUser .Text = (dr["Username"].ToString());
                    }
                    con.Close();
                    buttonForLog.Visible = true;
                    label4.Visible = true ;
                    label5.Visible = true ;
                    labelForPass.Visible = true;
                    labelForUser.Visible = true;
                    buttonForSubmit.Visible = false;
                  
                }

                else
                {
                    MessageBox.Show("Wrong answer.....");
                    textBoxForAns.Text = string.Empty;
                    textBoxForAns.Focus();
                }

            }
        }

        private void buttonForLog_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;

            userPassForm pass = new userPassForm();
            pass.Show();


        }
    }
}
