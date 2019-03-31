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
    public partial class FormAddPro : Form
    {
        string imgLoc = "";



        public FormAddPro()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

        private void FormAddPro_Load(object sender, EventArgs e)
        {
            labelAddProDate.Text = DateTime.Now.ToLongDateString();
            textBoxAddProId.Visible = false;
        }

        private void buttonAddProSave_Click(object sender, EventArgs e)
        {
            string Name = textBoxAddProName.Text;
            string Price = textBoxAddProPrice.Text;
            string Type = textBoxAddProType.Text;
            string Brand = textBoxAddProBrand.Text;
            string Quantity = textBoxAddProQuan.Text;
            string Date = labelAddProDate.Text;


            if (Name == "" || Price == "" || Type == "" || Brand == "" || Quantity == "")
            {
                MessageBox.Show("Please Input Missing Fields");
            }
            else if (pictureBoxAddPro.Image == null)
            {
                MessageBox.Show("Please choose a Photo");
                
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
                cmd.CommandText = "INSERT INTO addPro VALUES ( '" + Name + "', '" + Price + "', '" + Type + "' , '" + Brand + "', '" + Quantity + "' , '" + Date + "' ,@img)";
                cmd.Parameters.Add(new SqlParameter("@img", img));
                cmd.ExecuteNonQuery();


                cmd.CommandText = "Select ProductId from addPro where ProductName = '" + Name + "'";

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                   textBoxAddProId .Text  = (dr["ProductId"].ToString());
                }
                con.Close();
                textBoxAddProId.Visible = true;
                MessageBox.Show("Saved!");
                buttonAddProSave.Visible = false;
                buttonAddProUp.Visible = false;
            }


        }

        private void buttonAddProUp_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select a Photo";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imgLoc = dlg.FileName.ToString();
                pictureBoxAddPro.ImageLocation = imgLoc;

            }
            else
            {
                return;
            } 
        }

        private void buttonAddProBack_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;

            FormAdminDash dash = new FormAdminDash();
            dash.Show();
        }

        private void textBoxAddProPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsSeparator(e.KeyChar) ||
                Char.IsSymbol(e.KeyChar))

            {
                MessageBox.Show("Invalid Character!");
            }
            else if (e.Handled = Char.IsLetter(e.KeyChar))
            {
                MessageBox.Show("Can't contain letters!");
            }
        }

        private void textBoxAddProQuan_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void textBoxAddProQuan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsSeparator(e.KeyChar) ||
                Char.IsSymbol(e.KeyChar))

            {
                MessageBox.Show("Invalid Character!");
            }
            else if (e.Handled = Char.IsLetter(e.KeyChar))
            {
                MessageBox.Show("Can't contain letters!");
            }
        }

        private void textBoxAddProBrand_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxAddProType_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxAddProName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsPunctuation(e.KeyChar) ||
                
                 Char.IsSymbol(e.KeyChar))

            {
                MessageBox.Show("Invalid Character!");
            }
          
        }

        private void textBoxAddProId_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show("Can't do it!");
        }
    }
}
