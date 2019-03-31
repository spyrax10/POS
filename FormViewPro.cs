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
    public partial class FormViewPro : Form
    {

        SqlCommand cmd;
        SqlDataAdapter adapt;

        public FormViewPro()
        {
            InitializeComponent();
        }



        private void DisplayData()
        {
            SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("SELECT * FROM addPro", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }


        private void FormViewPro_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOS1DataSet.addPro' table. You can move, or remove it, as needed.
            this.addProTableAdapter.Fill(this.pOS1DataSet.addPro);
            labelDateView.Text = DateTime.Now.ToLongDateString();

            
            buttonDelView.Visible = false;
            DisplayData();
            SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select ProductName from addPro";

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBoxBrandView.Items.Add(dr[0].ToString());
            }
            
        }

        private void buttonDelView_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

            if (comboBoxBrandView.Text == "")
            {
                MessageBox.Show("Please fill the combobox.....");
                return;
            }
            else
            {
                if (MessageBox.Show("Are you sure to delete " + comboBoxBrandView.Text + " product?",
                         "",
                          MessageBoxButtons.YesNo,
                          MessageBoxIcon.Information) == DialogResult.Yes)

                {


                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Delete from addPro where ProductName = '" + comboBoxBrandView.Text + "'";


                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Deleted!");
                    comboBoxBrandView.Text = string.Empty;
                    DisplayData();
                    FormViewPro view = new FormViewPro();
                    this.Visible = false;
                    ShowInTaskbar = false;
                    view.Show();

                }
                else
                {
                    return;
                }
            }
        }

        private void buttonViewProBack_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;

            FormAdminDash dash = new FormAdminDash();
            dash.Show();
        }

        private void comboBoxBrandView_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            DisplayData();
            buttonDelView.Visible = true;
        }
    }
}
