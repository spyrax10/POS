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
    public partial class FormInvoice : Form
    {
        public FormInvoice()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");
        string cs = @"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True";
        private void FormInvoice_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOS1DataSet.invoiceTab2' table. You can move, or remove it, as needed.
            this.invoiceTab2TableAdapter.Fill(this.pOS1DataSet.invoiceTab2);

            dataGridView1.Visible = false;
            buttonReport.Visible = false;
         


        }

        public void Display_Data()
        {
            string Date = dateTimePicker1.Text;

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM invoiceTab2 WHERE Date = '" + Date + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBoxItems.Text = string.Empty;
            textBoxSales.Text = string.Empty;
            

        }

        private void buttonGo_Click(object sender, EventArgs e)
        {

   
                Display_Data();
                dataGridView1.Visible = true;
                textBoxSales.Visible = true;
                textBoxItems.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;



            int x = 0;
            decimal y = 0;

            foreach (DataGridViewRow r in dataGridView1 .Rows)
            {
                x += Convert.ToInt32(r.Cells[3].Value);
                textBoxItems .Text = x.ToString();
            }

            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                y += Convert.ToDecimal (r.Cells[4].Value);
                textBoxSales.Text = y.ToString();

            }

            if (dataGridView1.Rows.Count == 0)
            {
                buttonReport.Visible = false;
            }
            else
            {
                buttonReport.Visible = true;
            }

        }

        private void textBoxItems_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;

            FormAdminDash dash = new FormAdminDash();
            dash.Show();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = dateTimePicker1.Text;
            saveFile.Filter = "Text Files (*.txt) |*.txt| All Files (*.*) | *.* ";


            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFile.FileName);

                
                writer.Write("SALE REPORT AS OF : " +dateTimePicker1.Text);
                writer.Write(Environment.NewLine);
                writer.Write(Environment.NewLine);
                writer.Write("Total units sold: " + textBoxItems.Text + " units");
                writer.Write(Environment.NewLine);
                writer.Write("Total sale earned: " + "PHP "+ textBoxSales.Text);
                writer.Close();
                MessageBox.Show("Data Exported....");
            }
        }
    }
}
