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
    public partial class returnForm : Form
    {
        public returnForm()
        {
            InitializeComponent();
        }


   //     SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;
          
         

            FormPosMain pos = new FormPosMain();
            pos.Show();
           


        }

        private void returnForm_Load(object sender, EventArgs e)
        {
            labelCashName.Text = FormPosMain.passName;
            timer1.Start();
            labelRetDate.Text = DateTime.Now.ToLongDateString();
            labelRetTime.Text = DateTime.Now.ToLongTimeString();




            SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select ProductName from addPro";

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }

            con.Close();




        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelRetTime.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from addPro where ProductName = '" + comboBox1.Text + "'";

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {

                labelPosQuan.Text = (dr["ProductQuantity"].ToString());
                labelProId.Text = (dr["ProductId"].ToString());
            }

            con.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string Date = labelRetDate.Text;
            string Time = labelRetTime.Text;
          //  string IniQuan = labelPosQuan.Text;
            string Name = labelCashName.Text;
            string Pro = comboBox1.Text;
            //string FinQuan = textBox1.Text;

        


            if (comboBox1.Text == "")
            {
                MessageBox.Show("Select a Product First!");
                
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Enter Quantity");
                
            }

            else
            {




                SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");
                

                con.Open();

                double IniQuan = double.Parse(textBox1.Text) + double.Parse(labelPosQuan.Text);
                string FinQuan = IniQuan.ToString();
                

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE addPro SET ProductQuantity= '" + FinQuan.ToString()   + "' WHERE ProductId='" + labelProId.Text  + "'";
                cmd.ExecuteNonQuery();
                con.Close();
               
             

                con.Open();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from addPro where ProductName = '" + comboBox1.Text + "'";

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    labelPosQuan.Text = (dr["ProductQuantity"].ToString());

                }
                con.Close();

                con.Open();

             
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO voidTab VALUES ('" + labelRetDate.Text + "', '" + labelRetTime.Text + "', '" + labelCashName.Text + "', '" + comboBox1.Text + "', '" + textBox1 .Text + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                textBox1.Text = string.Empty;
                MessageBox.Show("Done...");





            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            viewReceipt  ret = new viewReceipt ();
            ret.Show();
        }
    }
}
