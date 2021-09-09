using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Main
{
    public partial class Form3 : Form
    {
        public class Player
        {
            public string name;
            public int score;
        }
        public Form3()
        {
            InitializeComponent();

        }
        private void Form3_Load(object sender, EventArgs e)
        {
            try
            {
                List<Player> listPlayer = new List<Player>();
                listPlayer = GetTop.getTop5Player();
                if (listPlayer == null)
                {
                    MessageBox.Show("NULL");

                }
                else
                {
                    lbPlayer1.Text = listPlayer[0].name.ToString();
                    lbPlayer2.Text = listPlayer[1].name.ToString();
                    lbPlayer3.Text = listPlayer[2].name.ToString();
                    lbPlayer4.Text = listPlayer[3].name.ToString();
                    lbPlayer5.Text = listPlayer[4].name.ToString();

                    lbScore1.Text = listPlayer[0].score.ToString();
                    lbScore2.Text = listPlayer[1].score.ToString();
                    lbScore3.Text = listPlayer[2].score.ToString();
                    lbScore4.Text = listPlayer[3].score.ToString();
                    lbScore5.Text = listPlayer[4].score.ToString();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi kết nối với cơ sở dữ liệu!");
                this.Close();
            }
        }
        Form form2;
        public Form Form_2
        {
            set { form2 = value; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            form2.Show();
        }

       
    }
}
