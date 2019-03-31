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
    public partial class userPassForm : Form
    {
        static int attempt = 3;


        public userPassForm()
        {
            InitializeComponent();
        }


        string cs = @"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True";

        private void buttonVerify_Click(object sender, EventArgs e)
        {

            if (textBoxAuthenUser.Text == "" || textBoxAuthenPass.Text == "" || textBoxAuthenRe.Text == "")
            {
                MessageBox.Show("Please input missing fields...");
            }
            else if (textBoxAuthenPass.Text != textBoxAuthenRe.Text)
            {
                MessageBox.Show("Password don't Match....");
                textBoxAuthenRe.Text = string.Empty;
                textBoxAuthenPass.Text = string.Empty;
                textBoxAuthenPass.Focus();
            }
            else
            {
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("Select * from authenTab where Username = @Username and Password = @Password and EmployeeId = @Id", con);
                cmd.Parameters.AddWithValue("@Username", textBoxAuthenUser.Text);
                cmd.Parameters.AddWithValue("@Password", textBoxAuthenPass.Text);
                cmd.Parameters.AddWithValue("@Id", labelUserId.Text);
                con.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                con.Close();


                int count = ds.Tables[0].Rows.Count;
                if (count == 1)
                {
                    attempt = 0;
                    con.Open();
                    cmd.CommandText = "Select * from addEmp2 where EmployeeId= '" + labelUserId .Text + "'";

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        labelType .Text = (dr["Position"].ToString());
                    }
                   
                    
                    else
                     {
                        MessageBox.Show("Wrong credentials.....");
                        --attempt;
                        textBoxAuthenUser.Text = string.Empty;
                        textBoxAuthenPass.Text = string.Empty;
                        textBoxAuthenRe.Text = string.Empty;
                        textBoxAuthenUser.Focus();
                      }
                    con.Close();
                    buttonVerify.Visible = false;
                    linkLabel1.Visible = false;
                    label4.Visible = true;
                    labelType.Visible = true;
                    buttonLog.Visible = true;
                    buttonLog.Focus();

                }
                else if ((attempt == 3) && (attempt > 0))
                {
                    MessageBox.Show("Wrong credentials.....");
                    --attempt;
                    textBoxAuthenUser.Text = string.Empty;
                    textBoxAuthenPass.Text = string.Empty;
                    textBoxAuthenRe.Text = string.Empty;
                    textBoxAuthenUser.Focus();


                }
                else if (attempt <= 0)
                {
                    linkLabel1.Visible = true;
                    textBoxAuthenUser.Text = string.Empty;
                    textBoxAuthenPass.Text = string.Empty;
                    textBoxAuthenRe.Text = string.Empty;
                    textBoxAuthenUser.Focus();

                }
                else
                {
                    MessageBox.Show("Wrong credentials.....");
                    --attempt;
                    textBoxAuthenUser.Text = string.Empty;
                    textBoxAuthenPass.Text = string.Empty;
                    textBoxAuthenRe.Text = string.Empty;
                    textBoxAuthenUser.Focus();
                }


            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            this.Visible = false;
            ShowInTaskbar = false;


            forgotForm forgot = new forgotForm();
            forgot.Show();
        }

        private void userPassForm_Load(object sender, EventArgs e)
        {
            label4.Visible = false;
            labelType.Visible = false;
            buttonLog.Visible = false;
            labelUserId.Text = FormLog .passingId;
            linkLabel1.Visible = false;
        }

        private void buttonLog_Click(object sender, EventArgs e)
        {
            if (labelType.Text == "Admin")
            {
                this.Hide();
                ShowInTaskbar = false;


                FormAdminDash admin = new FormAdminDash();
                admin.Show();
            }
            else if (labelType.Text == "Cashier")
            {
                this.Hide();
                ShowInTaskbar = false;


                FormUserProfile wel = new FormUserProfile();
                wel.Show();
            }
        }

        private void textBoxAuthenUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsPunctuation(e.KeyChar) ||
                   Char.IsSeparator(e.KeyChar) ||
                   Char.IsSymbol(e.KeyChar))

            {
                MessageBox.Show("Invalid Character!");
            }
        }

        private void textBoxAuthenPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsPunctuation(e.KeyChar) ||
                   Char.IsSeparator(e.KeyChar) ||
                   Char.IsSymbol(e.KeyChar))

            {
                MessageBox.Show("Invalid Character!");
            }
        }

        private void textBoxAuthenRe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsPunctuation(e.KeyChar) ||
                   Char.IsSeparator(e.KeyChar) ||
                   Char.IsSymbol(e.KeyChar))

            {
                MessageBox.Show("Invalid Character!");
            }
        }

   
    }
}
