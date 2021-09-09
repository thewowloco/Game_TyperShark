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
    public partial class Easy : Form
    {
        string playerName;
        static int fish_number = 6;//Số lượng cá trong level
        int current_fish_number = 6;//Số lượng cá còn lại trong level
        int point = 10;//Điểm của từng con cá
        public int sum_point;//Tổng điểm
        readonly Fish f = new Fish();//Dùng để lấy các thuộc tính chung của cá
        Fish[] fishes = new Fish[fish_number];
        int flag = 0;
        int flag1 = 0;
        int flagKey = 0;
        string[] word = new string[26] { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m" };
        int[] word_number = new int[6];
        int count = 0;
        public Easy(string name)
        {
            this.playerName = name;
            InitializeComponent();
        }
        int lose = 0;
        void Check(KeyEventArgs e,Label lb, int i)
        {
            if (e.KeyCode.ToString() == lb.Text)
            {
                fishes[i].is_alive = false;
                sum_point += point;//Cộng điểm
                this.Controls.Remove(lb);//Xóa bỏ cá khi gõ đúng
                current_fish_number--;//Trừ đi số lượng cá hiện tại
                lb.Text = "";// phục vụ data sau
            }
        }
        
        new void Move(Label lb, int i)
        {
            if (flag != 7 && flag1 == 0 && Lose(lb) == false && fishes[i].is_alive == true)
            {
                lb.Left += 1000;
                flag++;
            }
            if (flag != 7 && flag1 != 0 && Lose(lb) == false && fishes[i].is_alive == true)
            {
                lb.Left += 1700;
                flag++;
            }
            if (Lose(lb) == false && fishes[i].is_alive == true)
            {
                lb.Left -= f.Step;
            }
        }
        void set_start_position(int start)
        {
            f.Start_position = start;
        }
        void respawn(Label lb, int i,string w)
        {
            this.Controls.Add(lb);
            lb.Left = f.Start_position;
            fishes[i].is_alive = true;
            lb.Text = w;
           // current_fish_number++;
            flag = 1;
            flag1 = 1;
 
            count++;
        }
        bool Lose(Label lb)
        {
            if (lb.Left < -100 && lose == 0)
            {
                timer1.Enabled = false;
                lose++;
                //MessageBox.Show("Lose");
                pictureBox3.Show();
                MessageBox.Show("Chúc mừng " + playerName + ", bạn chơi được " + sum_point.ToString() + " điểm!");
                Console.WriteLine(playerName + "\t" + sum_point.ToString());
                try
                {
                    QueryData.insertInfoPlayer(playerName, sum_point);
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi kết nối Cơ sở dữ liệu!");
                    Console.WriteLine("Loi ket noi CSDL! dòng 152 - easy");
                }
                /*this.Close();*///Bo vao stick3
                timer3.Start();

                return true;
            }
            return false;
        }
        int flagPause = 0;
        private void Easy_KeyDown(object sender, KeyEventArgs e)
        {
            if (flagKey == 0)
            {
                Check(e, label1, 0);
                Check(e, label2, 1);
                Check(e, label3, 2);
                Check(e, label4, 3);
                Check(e, label5, 4);
                Check(e, label6, 5);
                if (current_fish_number == 0)
                {
                    timer2.Enabled = true;
                    current_fish_number = 6;
                }
                label8.Text = sum_point.ToString();
                
            }
            if (e.KeyCode == Keys.Space && flag2 == 1)
            {
                if (flagPause == 0)
                {
                    timer1.Stop();
                    pictureBox2.Show();
                    label9.Hide();
                    label11.Show();
                    flagPause++;
                }
                else
                {
                    timer1.Start();
                    pictureBox2.Hide();
                    label11.Hide();
                    label9.Show();
                    flagPause--;
                }
            }
        }
      
        private void timer1_Tick(object sender, EventArgs e)
        {
            Move(label1,0);
            Move(label2,1);
            Move(label3,2);
            Move(label4,3);
            Move(label5,4);
            Move(label6,5);

           
        }
        int flag2 = 0;
        private void Easy_Load(object sender, EventArgs e)
        {
            if (flag2 == 0)
            {
                timer1.Stop();
                timer2.Stop();
                label1.Hide();
                label2.Hide();
                label3.Hide();
                label4.Hide();
                label5.Hide();
                label6.Hide();
                label11.Hide();
                pictureBox2.Hide();
                pictureBox3.Hide();
               
            }
            f.Step = 10;
            set_start_position(label1.Left);
            for (int i = 0; i < fish_number; i++)
            {
                fishes[i] = new Fish();
            }

        }
        Random r = new Random();
        int n;
        void get_word_number()
        {
            for (int i = 0; i < fish_number; i++)
            {
                n = r.Next(0, 26);
                for (int j = 0; j < i; j++)
                {                   
                    if (n == word_number[j])
                    {
                        n = r.Next(0, 26);
                        j--;
                    }
                }
                word_number[i] = n;
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            get_word_number();
            respawn(label1, 0, word[word_number[count]].ToUpper());
            respawn(label2, 1, word[word_number[count]].ToUpper());
            respawn(label3, 2, word[word_number[count]].ToUpper());
            respawn(label4, 3, word[word_number[count]].ToUpper());
            respawn(label5, 4, word[word_number[count]].ToUpper());
            respawn(label6, 5, word[word_number[count]].ToUpper());
            timer2.Enabled = false;
            count = 0;
        }
        private void Easy_KeyUp(object sender, KeyEventArgs e)
        {
            if (flag2 == 0)
            {
                pictureBox1.Hide();
                
                timer1.Start();
                timer2.Start();
                label1.Show();
                label2.Show();
                label3.Show();
                label4.Show();
                label5.Show();
                label6.Show();
                
                flag2 = 1;
            }
        }
        

        private void Easy_MouseUp(object sender, MouseEventArgs e)
        {
            if (flag2 == 0)
            {
                pictureBox1.Hide();
                
                timer1.Start();
                timer2.Start();
                label1.Show();
                label2.Show();
                label3.Show();
                label4.Show();
                label5.Show();
                label6.Show();
                
                flag2 = 1;
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            if (flag1 != 0)
            {
                timer1.Stop();
                pictureBox2.Show();
                label9.Hide();
                label11.Show();
                flagKey++;
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            timer1.Start();
            pictureBox2.Hide();
            label11.Hide();
            label9.Show();
            flagKey--;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (flag2 == 0)
            {
                pictureBox1.Hide();

                timer1.Start();
                timer2.Start();
                label1.Show();
                label2.Show();
                label3.Show();
                label4.Show();
                label5.Show();
                label6.Show();

                flag2 = 1;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            pictureBox1.Hide();
            pictureBox3.Show();
            timer3.Start();
            timer1.Stop();
        }
    }
}
