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
    public partial class authenForm : Form
    {
        public authenForm()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

        private void buttonCont_Click(object sender, EventArgs e)
        {


            SqlCommand cm = new SqlCommand("Select * from authenTab where Username = @Username", con);
            cm.Parameters.AddWithValue("@Username", textBoxUser.Text);

            con.Open();
            SqlDataAdapter adapt = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            con.Close();
            int count = ds.Tables[0].Rows.Count;

            if (count == 1)
            {

                MessageBox.Show("Username already exist!");
                con.Close();
                textBoxUser.Text = string.Empty;
                textBoxUser.Focus();

            }

            else if (textBoxPass.Text != textBoxReType.Text)
            {
                MessageBox.Show("Password don't Match");
                textBoxPass.Text = string.Empty;
                textBoxReType.Text = string.Empty;

            }
         else if (textBoxUser.Text == "" || textBoxReType.Text == "" || textBoxPass.Text == "" || comboBoxSecQuest.Text == "" || textBoxAns.Text == "")
            {
                MessageBox.Show("Please fill the empty fields");
            }
            else
            {

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO authenTab VALUES ( '" + labelEmpId.Text + "', '" + labelFullname.Text + "', '" + textBoxUser.Text + "', '" + textBoxPass.Text + "' , '" + comboBoxSecQuest.Text + "', '" + textBoxAns.Text + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Done....");
                this.Visible = false;
                ShowInTaskbar = false;

                FormAdminDash add = new FormAdminDash();
                add.Show();


            }
            




        }

        private void authenForm_Load(object sender, EventArgs e)
        {
            labelEmpId.Text = FormAddEmp.passId;

            string First = FormAddEmp.passFirst;
            string Mid = FormAddEmp.passMid;
            string Last = FormAddEmp.passLast;

            labelFullname.Text = First + " " + Mid + " " + Last;


           

        }

        private void textBoxUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsPunctuation(e.KeyChar) ||
               Char.IsSeparator(e.KeyChar) ||
               Char.IsSymbol(e.KeyChar))
            {
                MessageBox.Show("Invalid Character");
            }
        }

        private void textBoxPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsPunctuation(e.KeyChar) ||
              Char.IsSeparator(e.KeyChar) ||
              Char.IsSymbol(e.KeyChar))
            {
                MessageBox.Show("Invalid Character");
            }
        }

        private void textBoxReType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsPunctuation(e.KeyChar) ||
              Char.IsSeparator(e.KeyChar) ||
              Char.IsSymbol(e.KeyChar))
            {
                MessageBox.Show("Invalid Character");
            }
        }

        private void textBoxAns_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsPunctuation(e.KeyChar) ||
              Char.IsSeparator(e.KeyChar) ||
              Char.IsSymbol(e.KeyChar))
            {
                MessageBox.Show("Invalid Character");
            }
        }
    }
}
