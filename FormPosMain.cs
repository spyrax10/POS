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
using System.Text.RegularExpressions;

namespace POS1
{
    public partial class FormPosMain : Form
    {
        public static string passtot;
        public static string passName;
        public static string receipt;
        public FormPosMain()
        {
            InitializeComponent();
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }
        private bool IntegerValidator(string input)
        {
            string pattern = "[^0-9]";
            if (Regex.IsMatch(input, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void FormPosMain_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label18.Text = DateTime.Now.ToLongTimeString();
            labelPosDate.Text = DateTime.Now.ToLongDateString();

            labelPosCashName.Text = FormUserProfile.fullname;
            label11.Text = FormUserProfile.ID;
           
            richTextBoxPosMain.AppendText("                          Spyrax10,INC.                    " + Environment.NewLine);
           
            richTextBoxPosMain.AppendText(Environment.NewLine);
            richTextBoxPosMain.AppendText("                         SALES INVOICE                    "+ Environment.NewLine);
            richTextBoxPosMain.AppendText(Environment.NewLine);
            richTextBoxPosMain.AppendText(Environment.NewLine);
            richTextBoxPosMain.AppendText("   " + "Date : " + labelPosDate.Text + Environment.NewLine);
            richTextBoxPosMain.AppendText(Environment.NewLine);
            richTextBoxPosMain.AppendText("   " + "Cashier : " + labelPosCashName.Text + "    " + Environment.NewLine);
            richTextBoxPosMain.AppendText(Environment.NewLine);
            richTextBoxPosMain.AppendText("   " + "ID No.: " + label11.Text + "  " + Environment.NewLine);
            richTextBoxPosMain.AppendText(Environment.NewLine);
            richTextBoxPosMain.AppendText(Environment.NewLine);
            richTextBoxPosMain.AppendText("  " + "Description" + "                                   Amount");

            labelCashPhp.Visible = false;
            labelChangePhp.Visible = false;
            buttonPrint.Visible = false;
            textBoxPosQuan.Visible = false;
            label1PosQuan.Visible = false;
            buttonPosOk.Visible = false;

            label1Cash.Visible = false;
            labelPosChange.Visible = false;
            textBoxPosCash.Visible = false;
            textBoxPosChange.Visible = false;
            labelTotal.Visible = false;
            textBoxTotalItems.Visible = false;
            buttonDel.Visible = false;
            buttonOk.Visible = false;
            buttonPosPay.Visible = false;


            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;


            SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select ProductName from addPro";

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBoxPosPro.Items.Add(dr[0].ToString());
            }




        }

        private void buttonPosPur_Click(object sender, EventArgs e)
        {
            if (comboBoxPosPro.Text == "")
            {
                MessageBox.Show("Please Select a Product!");
            }

            else
            {
                labelTotal.Visible = true;
                textBoxTotalItems.Visible = true;
                textBoxPosQuan.Visible = true;
                label1PosQuan.Visible = true;
                buttonPosOk.Visible = true;

            }
        }


        private void buttonPosOk_Click(object sender, EventArgs e)
        {
            buttonOk.Visible = true;
         
           


            string Quan = textBoxPosQuan.Text;
            string Name = labelPosMainName.Text;
            string Price = labelPosMainPrice.Text;
            string total = textBoxPosTot.Text;
            string left = labelPosLeftUnit.Text;


            try
            {
                if (labelPosLeftUnit.Text == "0")
                {
                    MessageBox.Show("Out of stock!");
                    textBoxPosQuan.Text = string.Empty;
                    return;
                }
                else if (textBoxPosQuan.Text == "0")
                {
                    MessageBox.Show("Invalid Input!");
                    textBoxPosQuan.Text = string.Empty;
                    return;
                }
                else if (comboBoxPosPro.Text == "")
                {
                    MessageBox.Show("Select a Product First!");
                    textBoxPosQuan.Text = string.Empty;
                    return;
                }
                else if ( textBoxPosQuan.Text == "" )
                {
                    MessageBox.Show("Input Quantity...");
                    textBoxPosQuan.Focus();
                    return;

                }

                else
                {
                    SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");


                    con.Open();

                    decimal minusQuan = decimal.Parse(labelPosLeftUnit.Text) - decimal.Parse(textBoxPosQuan.Text);
                    string minus = minusQuan.ToString();

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE addPro SET ProductQuantity= '" + minusQuan + "' WHERE ProductId='" + labelIdPro.Text + "'";

                    cmd.ExecuteNonQuery();
                    con.Close();
                    comboBoxPosPro.Text = string.Empty;

                    con.Open();


                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Select * from addPro where ProductId = '" + labelIdPro.Text + "'";

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {

                        labelPosLeftUnit.Text = (dr["ProductQuantity"].ToString());


                    }
                    con.Close();

                    decimal a = 0;
                    int b = 0;

                    decimal Total = decimal.Parse(labelPosMainPrice.Text) * decimal.Parse(textBoxPosQuan.Text);
                    dataGridViewPosMain.Rows.Add(Quan, Name, Price, Total, left);

                   

                    textBoxPosTot.Text = Total.ToString();

                    foreach (DataGridViewRow r in dataGridViewPosMain.Rows)
                    {
                        {
                            a += Convert.ToDecimal(r.Cells[3].Value);
                            textBoxPosGrandTot.Text = a.ToString();
                            textBoxPosQuan.Text = string.Empty;

                        }
                        {
                            b += Convert.ToInt32(r.Cells[0].Value);
                            textBoxTotalItems.Text = b.ToString();
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void buttonPosPay_Click(object sender, EventArgs e)
        {
            
            buttonPrint.Visible = true;
            label1Cash.Visible = true;
            labelPosChange.Visible = true;
            textBoxPosCash.Visible = true;
            textBoxPosChange.Visible = true;
            labelCashPhp.Visible = true;
            labelChangePhp.Visible = true;
        }

        private void comboBoxPosPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxPosQuan.Text = string.Empty;
            SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from addPro where ProductName = '" + comboBoxPosPro.Text + "'";

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {

                labelPosMainName.Text = (dr["ProductName"].ToString());
                labelPosMainPrice.Text = (dr["UnitPrice"].ToString());
                labelPosMainBrand.Text = (dr["ProductBrand"].ToString());
                labelPosMainType.Text = (dr["ProductType"].ToString());
                labelIdPro.Text = (dr["ProductId"].ToString());
                labelPosLeftUnit.Text = (dr["ProductQuantity"].ToString());



                byte[] img = (byte[])(dr["ProductImage"]);
                if (img == null)
                {
                    pictureBoxPosMain.Image = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(img);
                    pictureBoxPosMain.Image = Image.FromStream(ms);
                }
            }
        }

        private void labelPosMainPrice_Click(object sender, EventArgs e)
        {

        }

        private void labelPosMainBrand_Click(object sender, EventArgs e)
        {

        }

        private void labelPosMainType_Click(object sender, EventArgs e)
        {

        }

        private void textBoxPosTot_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPosGrandTot_TextChanged(object sender, EventArgs e)
        {

           
        }


        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");
            con.Open();

            if (textBoxPosCash.Text == "")
            {
                MessageBox.Show("Please Input Money");
                con.Close();
                return;
            }
           else if (decimal.Parse (textBoxPosCash.Text)  < decimal.Parse (textBoxPosGrandTot .Text) )
            {
                MessageBox.Show("Not Enough Money!");
                textBoxPosCash.Text = string.Empty;
                con.Close();
                return;
            }
            
            else
            {
                decimal Cash = decimal.Parse(textBoxPosCash.Text);
                decimal Grand = decimal.Parse(textBoxPosGrandTot.Text);
                decimal Change = Cash - Grand;
                textBoxPosChange.Text = Change.ToString();
                textBoxPosCash.ReadOnly = true;
                buttonPrint.Visible = false;
                buttonPosPay.Visible = false;
            }

            try
            {
                string Date = labelPosDate.Text;
      
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into invoiceTab2 Values ('" + Date + "','" + labelPosCashName.Text + "', '" + textBoxPosCash.Text + "','" + textBoxTotalItems.Text + "', '" + textBoxPosGrandTot.Text + "')";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "Select InvoiceId from invoiceTab2 where Cash = '" + textBoxPosCash.Text + "'";

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    labelInvoiceNo.Text = (dr["InvoiceId"].ToString());
                }
                con.Close();
                    richTextBoxPosMain.AppendText(Environment.NewLine);
                    richTextBoxPosMain.AppendText(Environment.NewLine);
                    richTextBoxPosMain.AppendText(Environment.NewLine);
                    richTextBoxPosMain.AppendText(Environment.NewLine);
                    richTextBoxPosMain.AppendText(Environment.NewLine);
                    richTextBoxPosMain.AppendText(" " + "Invoice Number:  " + labelInvoiceNo.Text + "    ");
                    richTextBoxPosMain.AppendText(Environment.NewLine);
                    richTextBoxPosMain.AppendText(Environment.NewLine);
                    richTextBoxPosMain.AppendText(" " + "Number of Items: " + textBoxTotalItems.Text  + "    " + Environment.NewLine);
                    richTextBoxPosMain.AppendText(Environment.NewLine);
                    richTextBoxPosMain.AppendText(" " + "Amount Due:  " + " P" + textBoxPosGrandTot.Text + "  " + Environment.NewLine);
                    richTextBoxPosMain.AppendText(Environment.NewLine);
                    richTextBoxPosMain.AppendText(" " + "Cash:  " + " P" + textBoxPosCash.Text + "  " + Environment.NewLine);
                    richTextBoxPosMain.AppendText(Environment.NewLine);
                    richTextBoxPosMain.AppendText(" " + "Change:  " + " P" + textBoxPosChange .Text + "  " + Environment.NewLine);
                    richTextBoxPosMain.AppendText(Environment.NewLine);
                    richTextBoxPosMain.AppendText(Environment.NewLine);
                    richTextBoxPosMain.AppendText("         THANK YOU FOR PURCHASING!           ");
                textBoxPosCash.ReadOnly = true;
                buttonPrint.Visible = false;
                buttonPosPay.Visible = false;
                textBoxPosGrandTot.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void dataGridViewPosMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            DataGridView data = new DataGridView();
            labelGrid.Text = dataGridViewPosMain.CurrentRow.Cells[1].Value.ToString();
            labelGridyun.Text = dataGridViewPosMain.CurrentRow.Cells[0].Value.ToString();
            comboBoxPosPro.Text = dataGridViewPosMain.CurrentRow.Cells[1].Value.ToString();
            buttonDel.Visible = true;


            SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from addPro where ProductName= '" + labelGrid.Text + "'";

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                labelLeft.Text = (dr["ProductQuantity"].ToString());
            }
            con.Close();
            label15.Visible = true ;
            label16.Visible = true ;
            label17.Visible = true ;
        }


        private void buttonDel_Click(object sender, EventArgs e)
        {
            decimal a = 0;
            int b = 0;


            

            if (labelGridyun.Text == "" || labelLeft.Text == "" || labelGridyun.Text == "")
            {
                MessageBox.Show("Click a Product.........");

                return;

            }


            else if (this.dataGridViewPosMain.SelectedRows.Count > 0)
            {
                dataGridViewPosMain.Rows.RemoveAt(this.dataGridViewPosMain.SelectedRows[0].Index);

                foreach (DataGridViewRow r in dataGridViewPosMain.Rows)
                {
                    {
                        a += Convert.ToDecimal (r.Cells[3].Value);
                        textBoxPosGrandTot.Text = a.ToString();
                        textBoxPosTot.Text = string.Empty;

                        this.richTextBoxPosMain.Update();
                        textBoxPosQuan.Text = string.Empty;
                    }

                    {

                        b += Convert.ToInt32(r.Cells[0].Value);
                        textBoxTotalItems.Text = b.ToString();

                    }

                }


                SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

                con.Open();
                decimal plusQuan = decimal.Parse(labelGridyun.Text) + decimal.Parse(labelLeft.Text);
                string plus = plusQuan.ToString();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE addPro SET ProductQuantity= '" + plus + "' WHERE ProductName='" + labelGrid.Text + "'";

                cmd.ExecuteNonQuery();
                con.Close();

                con.Open();


                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select * from addPro where ProductId = '" + labelIdPro.Text + "'";

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {

                    labelPosLeftUnit.Text = (dr["ProductQuantity"].ToString());


                }
                con.Close();
                buttonDel.Visible = false ;

                labelGrid.Text = string.Empty;
                labelGridyun.Text = string.Empty;
                labelLeft.Text = string.Empty;

                
                if (dataGridViewPosMain.Rows.Count == 0)
                {
                    textBoxPosGrandTot.Text = string.Empty;
                    textBoxPosTot.Text = string.Empty;
                   textBoxTotalItems.Text = string.Empty;



                  //  labelPosMainName.Text = string.Empty;
                   // labelPosMainPrice.Text = string.Empty;
                   // labelPosMainBrand.Text = string.Empty;
                   // labelPosMainType.Text = string.Empty;
                   // labelIdPro.Text = string.Empty;
                   // labelPosLeftUnit.Text = string.Empty;
                    //pictureBoxPosMain.Image = null;
                //    comboBoxPosPro.Items.Clear();

                }
                

            }

           


        }

      



        private void labelPosCashName_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {


        }

        private void labelIdPro_Click(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (textBoxPosGrandTot.Text != "")
            {
                MessageBox.Show("Complete the transaction first.....");
            }
            else 
            {
                if (MessageBox.Show("Continue?",
                         "",
                          MessageBoxButtons.YesNo,
                          MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    this.Hide();
                    FormPosMain posmain = new FormPosMain();
                    posmain.Show();
                }
                else
                {
                    return;
                }
            }
            
        }



  

        private void saveReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLog log = new FormLog ();
            log.Show();
        }
      

        private void buttonOk_Click(object sender, EventArgs e)
        {
            buttonPosPay.Visible = true;
          

            foreach (DataGridViewRow dr in dataGridViewPosMain.Rows)

            {

                richTextBoxPosMain.AppendText(Environment.NewLine);
                richTextBoxPosMain.AppendText(Environment.NewLine);
                richTextBoxPosMain.AppendText(Environment.NewLine);
                richTextBoxPosMain.AppendText("   " + dr.Cells[1].Value.ToString() + Environment.NewLine);
                richTextBoxPosMain.AppendText("       " + dr.Cells[0].Value.ToString() + " unit/s" + " @" + " " + " P" + dr.Cells[2].Value.ToString()  + "                   " + " P" + dr.Cells[3].Value.ToString());
                buttonPosPay.Focus();
                buttonPosPur.Visible = false;
                buttonDel.Visible = false;
                comboBoxPosPro.Items.Clear();
                buttonOk.Visible = false;
                labelGrid.Visible = false;
                labelGridyun.Visible = false;
                labelLeft.Visible = false;
                label15.Visible = false;
                label16.Visible = false;
                label17.Visible = false;

            }
            dataGridViewPosMain.Rows.Clear();
            labelTotal.Visible = false;
            textBoxTotalItems.Visible = false;
            textBoxPosQuan.Visible = false;
            label1PosQuan.Visible = false;
            buttonPosOk.Visible = false;

            labelPosMainName.Text = string.Empty;
            labelPosMainPrice.Text = string.Empty;
            labelPosMainBrand.Text = string.Empty;
            labelPosMainType.Text = string.Empty;
            labelIdPro.Text = string.Empty;
            labelPosLeftUnit.Text = string.Empty;
            pictureBoxPosMain.Image = null;
            textBoxPosTot.Text = string.Empty;
           
        }

        private void button1_Click_2(object sender, EventArgs e)
        {

            textBoxTotalItems.Text = string.Empty;
            textBoxPosTot.Text = string.Empty;
            textBoxPosGrandTot.Text = string.Empty;
        }

        private void textBoxPosQuan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsPunctuation(e.KeyChar) ||
                    Char.IsSeparator(e.KeyChar) ||
                    Char.IsSymbol(e.KeyChar))
                    
            {
                MessageBox.Show("Invalid Character!");
            }
            else if (e.Handled = Char.IsLetter (e.KeyChar ))
            {
                MessageBox.Show("Numbers Only!");
            }
          
        }

        private void textBoxPosCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = 
                    Char.IsSeparator(e.KeyChar) ||
                    Char.IsSymbol(e.KeyChar))
            {
                MessageBox.Show("Invalid Character!");
            }
            else if (e.Handled = Char.IsLetter(e.KeyChar))
            {
                MessageBox.Show("Numbers Only!");
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBoxPosChange.Text == "")
            {
                MessageBox.Show("Can't do it right now!");
                return;
            }
            else
            {

                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBoxPosMain.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, 120, 120);
        }

        private void logoutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {


            if(textBoxPosGrandTot.Text != "")
            {
                MessageBox.Show("Complete the transaction first.....");
            }
            else
            {
                this.Visible = false;
                ShowInTaskbar = false;
                FormLog log = new FormLog();
                log.Show();
            }

            
            
        }
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About_Box about = new About_Box();
            about.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label18.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void discardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (buttonPosPay.Visible == false)
            {
                MessageBox.Show("Can't do it right now.....");
                return;
            }
            else
            {

                returnForm ret = new returnForm();
                this.Visible = false;
                ShowInTaskbar = false;
                receipt = richTextBoxPosMain.Text;
                passName = labelPosCashName.Text;
                ret.Show();

            }

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_3(object sender, EventArgs e)
        {
           
           
        }

        private void textBoxTotalItems_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBoxPosMain_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
