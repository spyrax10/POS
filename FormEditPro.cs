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
using System.Drawing.Imaging;
using System.IO;

namespace POS1
{
    public partial class FormEditPro : Form
    {
        string imgLoc = "";

        public FormEditPro()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

        private void buttonEditProSave_Click(object sender, EventArgs e)
        {
            string Name = textBoxEditNamePro.Text;
            string Price = textBoxEditProPrice.Text.ToString();
            string Type = textBoxEditProType.Text;
            string Brand = textBoxEditProBrand.Text;
            string Quantity = textBoxEditProQuan.Text;
            string AddQuan = textBoxEditQuanPro.Text;
            string Date = labelDateEditPro.Text;
            string Id = labelEditProId.Text;


        

            if (Name == "" || Price == "" || Type == "" || Brand == "" || Quantity == "" )
            {
                MessageBox.Show("Fields cannot be empty");
                return;
            }
            else

            {
                SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

                double TotalQuan = double.Parse(textBoxEditProQuan.Text) + double.Parse(textBoxEditQuanPro .Text);
                 string Total = TotalQuan.ToString();

                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "UPDATE addPro SET ProductName= '" + Name + "', UnitPrice= '" + Price + "', ProductType= '" + Type + "' , ProductBrand= '" + Brand + "' , ProductQuantity= '" + Total + "' , DateAdded= '" + Date + "' WHERE ProductId='" + labelEditProId.Text  + "'";


                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("SAVED!");
                this.Visible = false;
                ShowInTaskbar = false;
                FormEditPro save = new FormEditPro();
                save.Show();



            }


        }

        private void FormEditPro_Load(object sender, EventArgs e)
        {
            labelEditProDate.Text = DateTime.Now.ToLongDateString();
           

            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select ProductName from addPro";

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBoxNameEditPro.Items.Add(dr[0].ToString());
            }
        }

        private void comboBoxNameEditPro_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from addPro where ProductName = '" + comboBoxNameEditPro .Text + "'";

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {

                textBoxEditNamePro .Text = (dr["ProductName"].ToString());
                textBoxEditProPrice .Text = (dr["UnitPrice"].ToString());
                textBoxEditProBrand .Text = (dr["ProductBrand"].ToString());
                textBoxEditProType .Text = (dr["ProductType"].ToString());
                labelEditProId.Text   = (dr["ProductId"].ToString());
                textBoxEditProQuan.Text = (dr["ProductQuantity"].ToString());
                labelDateEditPro .Text = (dr["DateAdded"].ToString());


                byte[] img = (byte[])(dr["ProductImage"]);
                if (img == null)
                {
                    pictureBoxEditPro .Image = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(img);
                    pictureBoxEditPro .Image = Image.FromStream(ms);
                }
            }
        }

        private void buttonEditProUp_Click(object sender, EventArgs e)
        {
            if (comboBoxNameEditPro.Text == "")
            {
                MessageBox.Show("Select a Product First!");
                return;
            }
            else
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Select a Photo";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    imgLoc = dlg.FileName.ToString();
                    pictureBoxEditPro.ImageLocation = imgLoc;
                
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

                        cmd.CommandText = "UPDATE addPro SET ProductImage=@img WHERE ProductName='" + comboBoxNameEditPro.Text + "'";

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
                        cmd.CommandText = "Select * from addPro where ProductName = '" + comboBoxNameEditPro.Text + "'";

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {

                            byte[] img = (byte[])(dr["ProductImage"]);
                            if (img == null)
                            {
                                pictureBoxEditPro.Image = null;
                            }
                            else
                            {
                                MemoryStream ms = new MemoryStream(img);
                                pictureBoxEditPro.Image = Image.FromStream(ms);
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

        }

        private void buttonEditProBack_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;

            FormAdminDash dash = new FormAdminDash();
            dash.Show();
        }

        private void buttonProSave_Click(object sender, EventArgs e)
        {
            


        }

        private void buttonProCancel_Click(object sender, EventArgs e)
        {

           
        }

        private void textBoxEditNamePro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsPunctuation(e.KeyChar) ||
                
                  Char.IsSymbol(e.KeyChar))

            {
                MessageBox.Show("Invalid Character!");
            }
        }

        private void textBoxEditProType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsPunctuation(e.KeyChar) ||
                  Char.IsSeparator(e.KeyChar) ||
                  Char.IsSymbol(e.KeyChar))

            {
                MessageBox.Show("Invalid Character!");
            }
            else if (e.Handled = Char.IsNumber (e.KeyChar ))
            {
                MessageBox.Show("Can't contain numbers!");
            }
        }

        private void textBoxEditProBrand_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxEditProQuan_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show("Can't do it!");
            
        }

        private void textBoxEditQuanPro_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxEditQuanPro_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void textBoxEditProPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxEditProPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsSeparator(e.KeyChar) ||
                  Char.IsSymbol(e.KeyChar))

            {
                MessageBox.Show("Invalid Character!");
            }
            else if (e.Handled = Char.IsLetter (e.KeyChar))
            {
                MessageBox.Show("Can't contain letters!");
            }
        }

        private void textBoxEditProQuan_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
