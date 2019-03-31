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
    public partial class FormLog : Form
    {
        
        public static string passingId;
        static int attempt = 3;
        public FormLog()
        {
            InitializeComponent();
        }

        string cs = @"Data Source=EMMANUELSAL\EMAN_SQL;Initial Catalog=POS1;Integrated Security=True";


        private void button1_Click(object sender, EventArgs e)
        {



        }
        
        private void FormLog_Load(object sender, EventArgs e)
        {
           
        }

        private void buttonCont_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                MessageBox.Show("Please Input your Id Number");
                textBox1.Focus();
                return;
            }
           

            try
            {
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("Select * from addEmp2 where EmployeeId = @EmployeeId", con);
                cmd.Parameters.AddWithValue("@EmployeeId", textBox1.Text);
                con.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                con.Close();


                int count = ds.Tables[0].Rows.Count;
                if (count == 1)
                {

                    attempt = 0;
                    passingId = textBox1.Text;
                    this.Visible = false;
                    ShowInTaskbar = false;

                    introForm  use = new introForm ();
                    use.Show();

                }
                else if (textBox1.Text == "10")
                {
                    this.Visible = false;
                    ShowInTaskbar = false;


                    FormAdminDash ad = new FormAdminDash();
                    ad.Show();

                }
                else if ((attempt == 3) && (attempt > 0))
                {
                    MessageBox.Show("Record Not Found!");
                    MessageBox.Show("You Have Only " + Convert.ToString(attempt) + " Attempt Left To Try");
                    --attempt;
                    textBox1.Text = string.Empty;
                    textBox1.Focus();
                }
                else if (attempt <= 0)
                {
                    this.Visible = false;
                    ShowInTaskbar = false;


                    timerForm time = new timerForm();
                    time.Show();

                }
                
                else
                {

                    MessageBox.Show("Record not found!");
                    MessageBox.Show("You Have Only " + Convert.ToString(attempt) + " Attempt Left To Try");
                    --attempt;

                    textBox1.Text = string.Empty;
                    textBox1.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = Char.IsPunctuation(e.KeyChar) ||
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
    }
}
