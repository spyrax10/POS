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
    public partial class returnItem : Form
    {
        public returnItem()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");
        string cs = @"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True";



        public void Display_Data()
        {
            string Date = dateTimePicker1.Text;

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM voidTab WHERE Date = '" + Date + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }


        private void returnItem_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOS1DataSet.voidTab' table. You can move, or remove it, as needed.
            this.voidTabTableAdapter.Fill(this.pOS1DataSet.voidTab);
            dataGridView1.Visible = false;
            
            textBoxRetItems.Visible = false;
            label2.Visible = false;
          





        }


        private void buttonGo_Click(object sender, EventArgs e)
        {

            Display_Data();
            dataGridView1.Visible = true;
            
            textBoxRetItems.Visible = true;
            label2.Visible = true;
        
            




            int h = 0;
            int g = 0;

            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                h += Convert.ToInt32(r.Cells[4].Value);
                textBoxRetItems.Text = h.ToString();
            }

          



        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBoxRetItems.Text = string.Empty;
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;

            FormAdminDash dash = new FormAdminDash();
            dash.Show();
        }
    }
}
