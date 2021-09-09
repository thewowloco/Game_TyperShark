using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace game
{
    
    public partial class Normal : Form
    {
        public string playerName;
        public int sum_point;//Tổng điểm
        int point = 30;//Điểm của từng con cá
        static int fish_number = 4;//Số lượng cá trong level
        static int current_fish_number = 4;//Số lượng cá còn lại trong level
        bool is_wrong_words  = true;
        bool first_words_correct = true;
        string lastest_key;
        string mid_key;
        Fish[,] fishes = new Fish[fish_number,2];//Dùng để xét từng chữ cá mang
        Fish[] f = new Fish[fish_number];//Xét tổng thể 1 cá
        int flag = 0;
        int flagKey = 0;
        public Normal(string name)
        {
            InitializeComponent();
            this.playerName = name;
        }
        int flag2 = 0;
        void Check_Keys(KeyEventArgs e, Label lb, Label lb1, Label lb2,GroupBox gb,int stt_fish)
        {
            String word = e.KeyCode.ToString();
            if (word == lb.Text && word != lastest_key && word != mid_key && fishes[stt_fish, 0].Is_typed == false)
            {
                lb.ForeColor = System.Drawing.Color.Yellow;
                fishes[stt_fish, 0].Is_typed = true;
                is_wrong_words = false;
                first_words_correct = true;
             
                return;
            }

            if (word == lb1.Text && fishes[stt_fish, 0].Is_typed == true && fishes[stt_fish, 1].Is_typed == false)
            {
                lb1.ForeColor = System.Drawing.Color.Yellow;
                fishes[stt_fish, 1].Is_typed = true;
                is_wrong_words = false;

                mid_key = word;
                return;
            }

            if (word == lb2.Text && fishes[stt_fish, 1].Is_typed == true )
            {
                lb2.ForeColor = System.Drawing.Color.Yellow;
                this.Controls.Remove(gb);
                is_wrong_words = false;
                current_fish_number -= 1;
                lastest_key = word;
                f[stt_fish].is_alive = false;
                lb.Text = null;
                lb1.Text = null;
                lb2.Text = null;
                sum_point += point;
                label21.Text = sum_point.ToString();
                return;
            }
            if (first_words_correct == false)
            {
                lb.ForeColor = System.Drawing.Color.Black;
                lb1.ForeColor = System.Drawing.Color.Black;
                lb2.ForeColor = System.Drawing.Color.Black;
                fishes[stt_fish, 0].Is_typed = false;
                fishes[stt_fish, 1].Is_typed = false;
              
            }

        }
        void respawn(Label lb, Label lb1, Label lb2, int stt_fish, GroupBox gb)
        {

            string randomStr = "XYZ";

            try
            {
                //Dùng chuỗi từ database 
                randomStr = QueryData.getRandomStringFromDb("normal");

                lb.Text = randomStr.Substring(0, 1);
                lb1.Text = randomStr.Substring(1, 1);
                lb2.Text = randomStr.Substring(2, 1);
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi kết nối Cơ sở dữ liệu!");
                Console.WriteLine("Loi ket noi CSDL!");
            }

            f[stt_fish].is_alive = true;
            this.Controls.Add(gb);
            lb.ForeColor = System.Drawing.Color.Black;
            lb1.ForeColor = System.Drawing.Color.Black;
            lb2.ForeColor = System.Drawing.Color.Black;
            gb.Left = f[0].Start_position+400;

        }
        void Rules(KeyEventArgs e,Label lb, Label lb1, Label lb2,GroupBox gb, int stt_fish)
        {
            first_words_correct = false;
            Check_Keys(e, lb, lb1, lb2,gb,stt_fish);
           
        }
        int flagPause = 0;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //normal
            is_wrong_words = true;
            label22.Text = "";
            if (flagKey == 1)
            {
                Rules(e, label1, label2, label3, groupBox1, 0);

                Rules(e, label4, label5, label6, groupBox2, 1);

                Rules(e, label7, label8, label9, groupBox3, 2);

                Rules(e, label10, label11, label12, groupBox4, 3);
                lastest_key = null;
                mid_key = null;

                if (is_wrong_words == true && flag2 == 1)
                {
                    label22.Text = "CHỮ NHẬP KHÔNG ĐÚNG !!";
                }
            }
            if (e.KeyCode == Keys.Space && flag2 == 1)
            {
                label22.Text = "";
                if (flagPause == 0)
                {
                    timer1.Stop();
                    pictureBox2.Show();
                    label20.Hide();
                    label18.Show();
                    flagPause++;
                    flagKey = 0;
                }
                else
                {
                    timer1.Start();
                    pictureBox2.Hide();
                    label18.Hide();
                    label20.Show();
                    flagPause--;
                    flagKey = 1;
                }
            }
        }
        
        void Moves(GroupBox gb)
        {
            if (flag != 5)
            {
                gb.Left += 800;
                flag++;
            }
            gb.Left -= f[0].Step;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Moves(groupBox1);
            Moves(groupBox2);
            Moves(groupBox3);
            Moves(groupBox4);
            if (groupBox1.Left < 0)
            {
                timer1.Enabled = false;
                MessageBox.Show("Chúc mừng " + playerName + ", bạn chơi được " + sum_point.ToString() + " điểm!");
                Console.WriteLine(playerName + "\t" + sum_point.ToString());

                try
                {
                    QueryData.insertInfoPlayer(playerName, sum_point);
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi kết nối Cơ sở dữ liệu!");
                    Console.WriteLine("Loi ket noi CSDL! dòng 193 - normal");
                }

                pictureBox3.Show();
                /*this.Close();*///Bo vao stick3
                timer3.Start();
            }
            if (current_fish_number <= 0)
            {
                timer2.Enabled = true;
            }
            else timer2.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (flag2 == 0)
            {
                timer1.Stop();
                timer2.Stop();
                groupBox1.Hide();
                groupBox2.Hide();
                groupBox3.Hide();
                groupBox4.Hide();
                label18.Hide();
                pictureBox2.Hide();
                pictureBox3.Hide();

            }
            for (int i = 0; i < fish_number; i++)
            {
                f[i] = new Fish();
                f[i].Start_position = 604+500;
                f[i].Step = 10;
            }
            for (int i = 0; i < fish_number; i++)
            {
                for (int j = 0; j < 2; j++)
                    fishes[i,j] = new Fish();
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            respawn(label1, label2, label3, 0,groupBox1);
            respawn(label4, label5, label6, 1, groupBox2);
            respawn(label7, label8, label9, 2, groupBox3);
            respawn(label10, label11, label12, 3, groupBox4);
            current_fish_number += fish_number;
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void Normal_KeyUp(object sender, KeyEventArgs e)
        {
            if (flag2 == 0)
            {
                pictureBox1.Hide();

                timer1.Start();
                groupBox1.Show();
                groupBox2.Show();
                groupBox3.Show();
                groupBox4.Show();
                flag2 = 1;
                flagKey = 1;
            }
        }

        private void Normal_MouseUp(object sender, MouseEventArgs e)
        {
            if (flag2 == 0)
            {
                pictureBox1.Hide();
                timer1.Start();
                groupBox1.Show();
                groupBox2.Show();
                groupBox3.Show();
                groupBox4.Show();
                flag2 = 1;
                flagKey = 1;
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {
            if (flag >0)
            {
                timer1.Stop();
                pictureBox2.Show();
                label20.Hide();
                label18.Show();
                flagKey--;
            }
        }
    

        private void label18_Click(object sender, EventArgs e)
        {
            
                timer1.Start();
                pictureBox2.Hide();
                label18.Hide();
                label20.Show();
                flagKey++;
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (flag2 == 0)
            {
                pictureBox1.Hide();
                timer1.Start();
                groupBox1.Show();
                groupBox2.Show();
                groupBox3.Show();
                groupBox4.Show();
                flag2 = 1;
                flagKey = 1;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            pictureBox1.Hide();
            pictureBox3.Show();
            timer3.Start();
            timer1.Stop();
        }
    }
}
