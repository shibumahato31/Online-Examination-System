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

namespace shibu
{
    public partial class Homepage : Form
    {
        public Homepage()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=SHIBU\\SQLEXPRESS;Initial Catalog=umu;Integrated Security=True");

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                studentpanel.Visible = false;
                teacherpanel.Visible = true;
                warrning.Visible = false;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                studentpanel.Visible = true;
                teacherpanel.Visible = false;
                warrning.Visible = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                password.PasswordChar = '\0';
                checkBox1.Text = "Hide password";
            }
            else
            {
                password.PasswordChar = '*';
                checkBox1.Text = "Show password";
            }
        }
        //Teachers login Button
        private void button2_Click(object sender, EventArgs e)
        {

            if ((name.Text.Equals("ss")) && (password.Text.Equals("ss")))
            {
                warrning.Visible = false;
                Admin form = new Admin();
                form.ShowDialog();
                //this.Hide();


            }
            else
            {
                warrning.Visible = true;
                name.Clear();
                password.Clear();
            }

        }
        //end

        private void Homepage_Load(object sender, EventArgs e)
        {
            teacherpanel.Visible = false;
            studentpanel.Visible = false;
            warrning.Visible = false;

        }
        //Students login Button
        public static string sendtext = "";
        private void button1_Click(object sender, EventArgs e)
        {
            sendtext = textBox2.Text;
            
            if ((textBox2.Text.Equals("Raj")) && (textBox1.Text.Equals("1952")))
            {
                warrning.Visible = false;
                Instructionpage form = new Instructionpage();
                form.ShowDialog();
                //this.Hide();
            
            }else if((textBox2.Text.Equals("rohit")) && (textBox1.Text.Equals("7739")))
    {

                warrning.Visible = false;
                Instructionpage form = new Instructionpage();
                form.ShowDialog();

            }
            else if((textBox2.Text.Equals("Prince")) && (textBox1.Text.Equals("8931")))
                {

                warrning.Visible = false;
                Instructionpage form = new Instructionpage();
                form.ShowDialog();
            }
            else
            {
                warrning.Visible = true;
                textBox1.Clear();
                textBox2.Clear();
            }
        }
             
            private void pictureBox2_Click_1(object sender, EventArgs e)
            {
                Application.Exit();
            }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                password.PasswordChar = '\0';
                checkBox1.Text = "Hide password";
            }
            else
            {
                password.PasswordChar = '*';
                checkBox1.Text = "Show password";
            }
        }

       
    }
    } 
