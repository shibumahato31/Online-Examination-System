using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;
using AForge.Controls;
using static System.Windows.Forms.DataFormats;
using System.IO;
using Microsoft.SqlServer.Server;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;

namespace shibu
{
    public partial class Exam : Form
    {
        public int islemdurumu = 0; //CAMERA STATUS
        FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        VideoCaptureDevice videoSource = null;
        public static int durdur = 0;
        public static int gondermesayisi = 0;
        public int kamerabaslat = 0;
        public int selected = 0;
        int second = 0;

        
        public Exam()
        {
            InitializeComponent();
            FetchQuestions();
        }
        SqlConnection conn = new SqlConnection("Data Source=SHIBU\\SQLEXPRESS;Initial Catalog=umu;Integrated Security=True");
        String ans1,ans2,ans3,ans4,ans5;
       
        private void exit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("           you have terminated the Exam            ");
            Exam a = new Exam();
            a.Close();
            Application.Exit();
           
            try
            {
                videoSource.SignalToStop();
                videoSource = null;
                if (!(videoSource == null))
                {
                    videoSource.Stop();
                    videoSource = null;
                }
            }
            catch { }
        }

        private void Exam_Load(object sender, EventArgs e)
        {
            second = 60;
            timer1.Start();

            label7.Text = Homepage.sendtext;
            try
            {
                this.label1.Text = "";
               //Enumerate all video input devices
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                {
                    label1.Text = "No local capture devices";
                }
                foreach (FilterInfo device in videoDevices)
                {
                    int i = 1;
                    comboBox1.Items.Add(device.Name);
                    label1.Text = ("camera" + i + "initialization completed..." + "\n");
                    i++;
                }
                comboBox1.SelectedIndex = 0;
            }
            catch (ApplicationException)
            {
                this.label1.Text = "No local capture devices";
                videoDevices = null;
            }


            selected = comboBox1.SelectedIndex;

            if (islemdurumu == 0)
            {


                if (kamerabaslat > 0) return;
                try
                {
                    videoSource = new VideoCaptureDevice(videoDevices[selected].MonikerString);
                    videoSource.NewFrame += new NewFrameEventHandler(Video_NewFrame);
                    videoSource.Start(); kamerabaslat = 1; //CAMERA STARTRED

                }
                catch
                {
                    MessageBox.Show("RESTART THE EXAM");
                    Application.Exit();

                    if (!(videoSource == null))
                        if (videoSource.IsRunning)
                        {
                            videoSource.SignalToStop();
                            videoSource = null;
                        }
                }//catch
            }
        }

        private void Video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap img = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = img;
        }

       

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            label5.Text = second--.ToString();
            if (second < 0)
            {
                timer1.Stop();

                Homepage home = new Homepage();
                home.ShowDialog();
            }
        }
       
        private void FetchQuestions()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(("SELECT * FROM Questions WHERE qno=1"), conn);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    groupBox1.Text = dr["questions"].ToString();
                    radioButton1.Text = dr["options1"].ToString();
                    radioButton2.Text = dr["option2"].ToString();
                    radioButton3.Text = dr["option3"].ToString();
                    radioButton4.Text = dr["option4"].ToString();
                    ans1 = dr["ans"].ToString();
                }
                
                 cmd = new SqlCommand(("SELECT * FROM Questions WHERE qno=2"), conn);
                 dt = new DataTable();
                 sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    groupBox2.Text = dr["questions"].ToString();
                    radioButton8.Text = dr["options1"].ToString();
                    radioButton7.Text = dr["option2"].ToString();
                    radioButton6.Text = dr["option3"].ToString();
                    radioButton5.Text = dr["option4"].ToString();
                    ans2 = dr["ans"].ToString();
                }
                cmd = new SqlCommand(("SELECT * FROM Questions WHERE qno=3"), conn);
                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    groupBox3.Text = dr["questions"].ToString();
                    radioButton12.Text = dr["options1"].ToString();
                    radioButton11.Text = dr["option2"].ToString();
                    radioButton10.Text = dr["option3"].ToString();
                    radioButton9.Text = dr["option4"].ToString();
                    ans3 = dr["ans"].ToString();
                }
                cmd = new SqlCommand(("SELECT * FROM Questions WHERE qno=4"), conn);
                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    groupBox4.Text = dr["questions"].ToString();
                    radioButton16.Text = dr["options1"].ToString();
                    radioButton15.Text = dr["option2"].ToString();
                    radioButton14.Text = dr["option3"].ToString();
                    radioButton13.Text = dr["option4"].ToString();
                    ans4 = dr["ans"].ToString();
                }
                cmd = new SqlCommand(("SELECT * FROM Questions WHERE qno=5"), conn);
                dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    groupBox5.Text = dr["questions"].ToString();
                    radioButton20.Text = dr["options1"].ToString();
                    radioButton19.Text = dr["option2"].ToString();
                    radioButton18.Text = dr["option3"].ToString();
                    radioButton17.Text = dr["option4"].ToString();
                    ans5 = dr["ans"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //start
        int Result = 0;
        String[] s = new string[10];
        private void score1()
        {
            if (radioButton1.Checked)
            {
                s[0] = "";
                s[0] = radioButton1.Text;
            }else if (radioButton2.Checked)
            {
                s[0] = "";
                s[0] = radioButton2.Text;
            }
            else if (radioButton3.Checked)
            {
                s[0] = "";
                s[0] = radioButton3.Text;
            }
            else if (radioButton4.Checked)
                {
                s[0] = "";
                s[0] = radioButton4.Text;
            } 
            if (s[0] == ans1)
            {
                Result = Result+1;
            }
            else
            {
                Result = Result;
            }
            
        }
        private void score2()
        {
            if (radioButton5.Checked)
            {
                s[1] = "";
                s[1] = radioButton5.Text;
            }
            else if (radioButton6.Checked)
            {
                s[1] = "";
                s[1] = radioButton6.Text;
            }
            else if (radioButton7.Checked)
            {
                s[1] = "";
                s[1] = radioButton7.Text;
            }
            else if (radioButton8.Checked)
            {
                s[1] = "";
                s[1] = radioButton8.Text;
            }
            if (s[1] == ans2)
            {
                Result = Result + 1;
            }
            else
            {
                Result = Result ;
            }
        }
        private void score3()
        {
            if (radioButton9.Checked)
            {
                s[3] = "";
                s[3] = radioButton9.Text;
            }
            else if (radioButton10.Checked)
            {
                s[3] = "";
                s[3] = radioButton10.Text;
            }
            else if (radioButton11.Checked)
            {
                s[3] = "";
                s[3] = radioButton11.Text;
            }
            else if (radioButton12.Checked)
            {
                s[3] = "";
                s[3] = radioButton12.Text;
            }
            if (s[3] == ans3)
            {
                Result = Result + 1;
            }
            else
            {
                Result = Result;
            }
        }

       

        private void score4()
        {
            if (radioButton13.Checked)
            {
                s[4] = "";
                s[4] = radioButton13.Text;
            }
            else if (radioButton14.Checked)
            {
                s[4] = "";
                s[4] = radioButton14.Text;
            }
            else if (radioButton15.Checked)
            {
                s[4] = "";
                s[4] = radioButton15.Text;
            }
            else if (radioButton16.Checked)
            {
                s[4] = "";
                s[4] = radioButton16.Text;
            }
            if (s[4] == ans4)
            {
                Result = Result + 1;
            }
            else
            {
                Result = Result;
            }
        }
        private void score5()
        {
            if (radioButton17.Checked)
            {
                s[5] = "";
                s[5] = radioButton17.Text;
            }
            else if (radioButton18.Checked)
            {
                s[5] = "";
                s[5] = radioButton18.Text;
            }
            else if (radioButton16.Checked)
            {
                s[5] = "";
                s[5] = radioButton19.Text;
            }
            else if (radioButton20.Checked)
            {
                s[5] = "";
                s[5] = radioButton20.Text;
            }
            if (s[5] == ans5)
            {
                Result = Result + 1;
            }
            else
            {
                Result = Result;
            }
        }
        
        private void InsertResult()
        {
            try
            {
                //conn.Open();
                string Std_name = label7.Text;
                SqlCommand cmd = new SqlCommand("INSERT INTO Result(Std_name, Result) VALUES('" + Std_name + "','" + Result + "')", conn);
                cmd.Parameters.AddWithValue("Std_name", label7.Text);
                cmd.Parameters.AddWithValue("Result", Result);
                if (conn.State == ConnectionState.Closed)
                { conn.Open(); 
                }
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //start
           Result = 0;
            score1();
            score2();
            score3();
            score4();
            score5();
            InsertResult();
           // MessageBox.Show("" + Result);

            //end
            MessageBox.Show("YOU HAVE SUCCESSFULLY COMPLETED");
            if (!(videoSource == null))
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }
            Homepage home = new Homepage();
            home.ShowDialog();

        }
    }
}
