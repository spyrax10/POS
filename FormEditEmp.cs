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
    public partial class FormEditEmp : Form
    {
        string imgLoc = "";
        public static string first;
        public static string middle;
        public static string last;
        public static string ID;
        public FormEditEmp()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

        private void FormEditEmp_Load(object sender, EventArgs e)
        {
            labelDateEditEmp .Text = DateTime.Now.ToLongDateString();


            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select EmployeeId from addEmp2";

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBoxEditEmpId .Items.Add(dr[0].ToString());
            }

        }

        private void comboBoxEditEmpId_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from addEmp2 where EmployeeId = '" + comboBoxEditEmpId.Text + "'";

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {

                textBoxEditEmpFirst.Text = (dr["Firstname"].ToString());
                textBoxEditEmpMiddle.Text = (dr["Middlename"].ToString());
                textBoxEditEmpLast.Text = (dr["Lastname"].ToString());
                textBoxEditEmpSex .Text = (dr["Sex"].ToString());
                textBoxEditEmpAdd.Text = (dr["Address"].ToString());
                textBoxEditEmpMob.Text = (dr["Mobile"].ToString());
                textBoxEditEmpPos.Text = (dr["Position"].ToString());
                textBoxEditEmpStat .Text = (dr["Status"].ToString());
                labelEditEmpDate.Text = (dr["DateJoin"].ToString());

                byte[] img = (byte[])(dr["Image"]);
                if (img == null)
                {
                    pictureBoxEditEmp.Image = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(img);
                    pictureBoxEditEmp.Image = Image.FromStream(ms);
                }
                con.Close();
            }

        }

        private void buttonEditSaveEmp_Click(object sender, EventArgs e)
        {
            string First = textBoxEditEmpFirst.Text;
            string Middle = textBoxEditEmpMiddle.Text;
            string Last = textBoxEditEmpLast.Text;
            string Sex = textBoxEditEmpSex.Text;
            string Add = textBoxEditEmpAdd.Text;
            string Mobile = textBoxEditEmpMob.Text;
            string Position = textBoxEditEmpPos.Text;
            string Stat = textBoxEditEmpStat.Text;


            if (First == "" || Middle == "" || Last == "" || Sex == "" || Add == "" || Mobile == "" || Position == "" || Stat == "")
            {
                MessageBox.Show("Fields cannot be empty!");
                return;
            }
            else if (textBoxEditEmpMob.Text.Length <= 10)
            {
                MessageBox.Show("Please Input 11 digit number");
                textBoxEditEmpMob.Text = string.Empty;
            }
            else
            {
                SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE addEmp2 SET Firstname = '" + First + "', Middlename = '" + Middle + "', Lastname = '" + Last + "', Sex = '" + Sex + "', Address = '" + Add + "', Mobile = '" + Mobile + "', Position = '" + Position + "', Status = '" + Stat + "', DateJoin = '" + labelEditEmpDate.Text + "' WHERE EmployeeId = '" + comboBoxEditEmpId.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Success!");

            }




        }

        private void buttonAddEmpUp_Click(object sender, EventArgs e)
        {

            if(comboBoxEditEmpId .Text == "")
            {
                MessageBox.Show("Select an Employee first!");
                return;
            }


            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select a Photo";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imgLoc = dlg.FileName.ToString();
                pictureBoxEditEmp .ImageLocation = imgLoc;

                if (MessageBox.Show("Save?",
                "",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");


                    byte[] img = null;
                    FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    img = br.ReadBytes((int)fs.Length);



                    con.Open();

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "UPDATE addEmp2 SET Image=@img WHERE EmployeeId ='" + comboBoxEditEmpId .Text + "'";

                    cmd.Parameters.Add(new SqlParameter("@img", img));

                    cmd.ExecuteNonQuery();

                    con.Close();
                    MessageBox.Show("Done!");
                }
                else
                {
                    SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

                    con.Open();

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * from addEmp2 where EmployeeId = '" + comboBoxEditEmpId .Text + "'";

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {

                        byte[] img = (byte[])(dr["Image"]);
                        if (img == null)
                        {
                            pictureBoxEditEmp .Image = null;
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream(img);
                            pictureBoxEditEmp .Image = Image.FromStream(ms);
                        }
                    }
                    con.Close();
                }



            }
            else
            {

                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Visible = false;
            ShowInTaskbar = false;

            FormAdminDash dash = new FormAdminDash();
            dash.Show();
        }

        private void textBoxEditEmpFirst_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxEditEmpMiddle_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxEditEmpLast_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxEditEmpSex_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxEditEmpPos_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxEditEmpStat_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxEditEmpMob_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxEditEmpMob_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsPunctuation(e.KeyChar) ||
               Char.IsSeparator(e.KeyChar) ||
               Char.IsSymbol(e.KeyChar))

            {
                MessageBox.Show("Invalid Character!");
            }
            else if (e.Handled = Char.IsLetter(e.KeyChar))
            {
                MessageBox.Show("Can't contain letter!");
            }
            else if (textBoxEditEmpMob.Text.Length >= 11)

            {
                MessageBox.Show("Password Too Long!");
                textBoxEditEmpMob.Text = string.Empty;

            }
        }

        private void buttonUser_Click(object sender, EventArgs e)
        {
            first = textBoxEditEmpFirst.Text;
            middle = textBoxEditEmpMiddle.Text;
            last = textBoxEditEmpLast.Text;
            ID = comboBoxEditEmpId.Text;

            if (comboBoxEditEmpId.Text == "")
            {
                MessageBox.Show("Select an ID....");
            }
            else
            {

                this.Visible = false;
                ShowInTaskbar = false;

                editForm edit = new editForm();
                edit.Show();
            }
        }
    }
}

