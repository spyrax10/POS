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
    public partial class FormViewEmp : Form
    {
        public FormViewEmp()
        {
            InitializeComponent();
        }

        SqlCommand cmd;
        SqlDataAdapter adapt;

        private void FormViewEmp_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOS1DataSet.addEmp2' table. You can move, or remove it, as needed.
            this.addEmp2TableAdapter.Fill(this.pOS1DataSet.addEmp2);
            label2.Text = DateTime.Now.ToLongDateString();
            buttonEmpDel.Visible = false;

            SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");

            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select EmployeeId from addEmp2";

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBoxLast.Items.Add(dr[0].ToString());
            }


        }

        private void DisplayData()
        {
            SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from addEmp2", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void buttonEmpDel_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True");


            if(comboBoxLast.Text == "")
            {
                MessageBox.Show("Nothing Found!");
                return;
            }



          else
            {

                if (MessageBox.Show("Are you sure to delete " + comboBoxLast.Text + " record?",
                         "",
                          MessageBoxButtons.YesNo,
                          MessageBoxIcon.Information) == DialogResult.Yes)
              
                {

                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Delete from addEmp2 where EmployeeId = '" + comboBoxLast.Text + "'";


                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Deleted!");
                    comboBoxLast.Text = string.Empty;
                    DisplayData();
                    FormViewEmp view = new FormViewEmp();
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

        private void buttonViewEmpBack_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowInTaskbar = false;

            FormAdminDash dash = new FormAdminDash();
            dash.Show();
        }

        private void comboBoxLast_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonEmpDel.Visible = true;
        }
    }
}