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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Red;
        }

        private void label3_MouseMove(object sender, MouseEventArgs e)
        {
            label3.ForeColor = Color.BurlyWood;
        }
        // tao user
        private void label3_Click(object sender, EventArgs e)
        {
            if (txtCreatUser.Text == "")
                MessageBox.Show("Không được để trống");
            else
            {
                listBox1.Items.Add(txtCreatUser.Text);
                txtCreatUser.Text = "";
            }

        }

        private void login_Load(object sender, EventArgs e)
        {
            txtCreatUser.Focus();
            listBox1.Items.Clear();
            try
            {
                List<Form3.Player> listPlayer = new List<Form3.Player>();
                listPlayer = GetTop.getPlayerNameList();

                if (listPlayer == null)
                {
                    MessageBox.Show("NULL");
                }
                else
                {
                    for (int i = 0; i < listPlayer.Count; i++)
                    {
                        listBox1.Items.Add(listPlayer[i].name);
                    }
                }
                listBox1.SelectedIndex = listPlayer.Count - 1;
                string name = listBox1.SelectedItem.ToString();
                yourName(name);
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi kết nối với cơ sở dữ liệu!");
                Console.WriteLine("Loi!");
                this.Close();
            }

        }
        /// button3 la nut xoa
        private void button3_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index >= 0) listBox1.Items.RemoveAt(index);
        }
        /// button2 la nut ok

        /// lay du lieu qua lai 2 form trong cho your name va cho chon nguoi dung
        public delegate void GETDATA(string name);
        public GETDATA yourName;
        // nut dong
        private void button1_Click(object sender, EventArgs e)
        {
            int index = -1;
            index = listBox1.SelectedIndex;
            //MessageBox.Show(" "+ index);
            if (index >= 0)
                this.Close();
            else
                MessageBox.Show("Bạn Phải chọn người chơi");
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int index = -1;
            index = listBox1.SelectedIndex;
            //MessageBox.Show(" "+ index);
            if (index >= 0)
            {
                string name = listBox1.SelectedItem.ToString();
                yourName(name);
                this.Close();
            }
            else
                MessageBox.Show("Bạn Phải chọn người chơi");
        }

        private void txtCreatUser_TextChanged(object sender, EventArgs e)
        {
            if(txtCreatUser.TextLength == 25)
            {
                txtCreatUser.Text = txtCreatUser.Text.Remove(txtCreatUser.TextLength - 1, 1);
                txtCreatUser.SelectionStart = (txtCreatUser.MaxLength - 1);
            }
        }
    }
}
