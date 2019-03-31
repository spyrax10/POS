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
using System.Globalization;
using System.Threading;


namespace POS1
{
    public partial class FormAddEmp : Form
    {
        string imgLoc = "";

        public static string passFirst;
        public static string passMid;
        public static string passLast;
        public static string passId;

        public FormAddEmp()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

       

        private void FormAddEmp_Load(object sender, EventArgs e)
        {
            labelDateAddEmp.Text = DateTime.Now.ToLongDateString();
            textBoxAddEmpId.Visible = false;
        }

       

        private void buttonSaveEmp_Click(object sender, EventArgs e)
        {
            string First = textBoxAddEmpFirst.Text;
            string Middle = textBoxAddEmpMiddle.Text;
            string Last = textBoxAddEmpLast.Text;
            string Sex = comboBoxAddEmpSex.Text;
            string Add = textBoxAddEmpAdd.Text;
            string Mobile = textBoxAddEmpMob.Text;
            string Position = textBoxAddEmpPos.Text;
            string Status = comboBoxAddEmpStat.Text;
            string Date = labelDateAddEmp.Text;


            if (First == "" || Middle == "" || Last == "" || Sex == "" || Add == "" || Mobile == "" || Position == "" || Status == "")
            {
                MessageBox.Show("Please Input Missing fields!");
            }
            else if (pictureBoxAddEmp.Image == null)
            {
                MessageBox.Show("Please Upload a Photo");
            }
            else if (textBoxAddEmpMob.Text.Length  <= 10)
            {
                MessageBox.Show("Please Input 11 digit number");
                textBoxAddEmpMob.Text = string.Empty;
            }

            else
            {

                byte[] img = null;
                FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into addEmp2 Values ( '" + First + "', '" + Middle  + "', '" + Last + "' , '" + Sex + "', '" + Add + "' , '" + Mobile + "' , '" + Position + "', '" + Status + "', '" + Date + "'        ,@img)";
                cmd.Parameters.Add(new SqlParameter("@img", img));
                cmd.ExecuteNonQuery();

                cmd.CommandText = "Select EmployeeId from addEmp2 where Lastname = '" + Last + "' and Firstname = '" + First + "'";

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    textBoxAddEmpId .Text = (dr["EmployeeId"].ToString());
                }
                con.Close();
                textBoxAddEmpId.Visible = true;
                buttonSaveEmp.Visible = false;
                buttonAddEmpUp.Visible = false;

               
                passFirst = textBoxAddEmpFirst.Text;
                passMid = textBoxAddEmpMiddle.Text;
                passLast = textBoxAddEmpLast.Text;
                passId = textBoxAddEmpId.Text;


                MessageBox.Show("Create your Username........");

                authenForm authen = new authenForm();
                authen.Show();
                this.Visible = false;
                ShowInTaskbar = false;


            }

        }         

        private void buttonAddEmpUp_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select a Photo";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imgLoc = dlg.FileName.ToString();
                pictureBoxAddEmp .ImageLocation = imgLoc;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;

            FormAdminDash dash = new FormAdminDash();
            dash.Show();
        }

        private void textBoxAddEmpFirst_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsPunctuation(e.KeyChar) ||
                Char.IsSeparator(e.KeyChar) ||
                Char.IsSymbol(e.KeyChar))

            {
                MessageBox.Show("Invalid Character!");
            }
            else if (e.Handled = Char.IsNumber(e.KeyChar))
            {
                MessageBox.Show("Can't contain numbers!");
            }
        }

        private void textBoxAddEmpMiddle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsPunctuation(e.KeyChar) ||
                Char.IsSeparator(e.KeyChar) ||
                Char.IsSymbol(e.KeyChar))

            {
                MessageBox.Show("Invalid Character!");
            }
            else if (e.Handled = Char.IsNumber(e.KeyChar))
            {
                MessageBox.Show("Can't contain numbers!");
            }
        }

        private void textBoxAddEmpLast_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsPunctuation(e.KeyChar) ||
                Char.IsSeparator(e.KeyChar) ||
                Char.IsSymbol(e.KeyChar))

            {
                MessageBox.Show("Invalid Character!");
            }
            else if (e.Handled = Char.IsNumber(e.KeyChar))
            {
                MessageBox.Show("Can't contain numbers!");
            }
        }

        private void textBoxAddEmpPos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsPunctuation(e.KeyChar) ||
                Char.IsSeparator(e.KeyChar) ||
                Char.IsSymbol(e.KeyChar))

            {
                MessageBox.Show("Invalid Character!");
            }
            else if (e.Handled = Char.IsNumber(e.KeyChar))
            {
                MessageBox.Show("Can't contain numbers!");
            }
        }

        private void textBoxAddEmpMob_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsPunctuation(e.KeyChar) ||
                Char.IsSeparator(e.KeyChar) ||
                Char.IsSymbol(e.KeyChar))

            {
                MessageBox.Show("Invalid Character!");
            }
            else if (e.Handled = Char.IsLetter(e.KeyChar))
            {
                MessageBox.Show("Can't contain letters!");
            }
            else if (textBoxAddEmpMob.Text.Length >= 11)

            {
                MessageBox.Show("Password Too Long!");
                textBoxAddEmpMob .Text = string.Empty;

            }
        }
    }
 }

