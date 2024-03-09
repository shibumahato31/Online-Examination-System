using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace shibu
{
    public partial class Admin : Form
    {
       
        public Admin()
        {
            InitializeComponent();

        }
        
        SqlConnection conn = new SqlConnection("Data Source=SHIBU\\SQLEXPRESS;Initial Catalog=umu;Integrated Security=True");
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void Admin_Load(object sender, EventArgs e)
        {
            AddQuestion.Visible = false;
            delete.Visible = false;
            Viewquestion.Visible = false;
            Viewresult.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            AddQuestion.Visible = true;
            delete.Visible = false;
            Viewquestion.Visible = false;
            Viewresult.Visible = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            AddQuestion.Visible = false;
            delete.Visible = true;
            Viewquestion.Visible = false;
            Viewresult.Visible = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("            Thank You            ");
            Admin a = new Admin();
            a.Close();
            Homepage home = new Homepage();
            home.ShowDialog();

        }

        private void Add_Click(object sender, EventArgs e)
        {
            try
            {
                int Question_no = int.Parse(txtqno.Text);
                string Question = txtquestion.Text;
                string option1 = txtoption1.Text;
                string Option2 = txtoption2.Text;
                string Option3 = txtoption3.Text;
                string Option4 = txtoption4.Text;
                string Answer = txtans.Text;
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Questions(qno, questions, options1, option2, option3, option4, ans) VALUES('" + Question_no + "','" + Question + "','" + option1 + "','" + Option2 + "','" + Option3 + "','" + Option4 + "','" + Answer + "')", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("you have Successfully Added One Quedtion ");
            txtqno.Clear();
            txtquestion.Clear();
            txtoption1.Clear();
            txtoption2.Clear();
            txtoption3.Clear();
            txtoption4.Clear();
            txtans.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int Question_no = int.Parse(textBox1.Text);

                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Questions WHERE qno = '" + Question_no + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("you have Deleted One Quedtion ");
            textBox1.Clear();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Viewquestion.Visible = true;
            AddQuestion.Visible = false;
            delete.Visible = false;
            Viewresult.Visible = false;
            ViewQuestions();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Viewquestion.Visible = false;
            AddQuestion.Visible = false;
            delete.Visible = false;
            Viewresult.Visible = true;
            ViewResult();
        }
        public void ViewResult()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Result", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            dataGridView2.DataSource = dt;
        }

       
        public void ViewQuestions()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Questions", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            dataGridView1.DataSource = dt;
        }

        
    }
}
