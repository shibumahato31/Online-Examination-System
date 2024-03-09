using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shibu
{
    public partial class Instructionpage : Form
    {
        public Instructionpage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked && checkBox2.Checked)
            {
                Exam ex = new Exam();
                ex.ShowDialog();
                this.Hide();

            }
            else if (checkBox1.Checked && !checkBox2.Checked)
            {
                MessageBox.Show("please allow Microphone");
            }
            else if (!checkBox1.Checked)
            {
                MessageBox.Show("please allow webcam");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("        you have not given your Exam            ");
            Instructionpage a = new Instructionpage();
            a.Close();
            Homepage home = new Homepage();
            home.ShowDialog();
        }
    }
}
