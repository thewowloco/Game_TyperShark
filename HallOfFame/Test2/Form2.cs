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
using Tutorial;

namespace Main
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        bool exit = false;
        private void Quit(object sender, FormClosingEventArgs e)
        {
            if (exit)
            {
                if (MessageBox.Show("Bạn có muốn đóng Game", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    e.Cancel = true;
            }
        }

        private void btQuit_Click(object sender, EventArgs e)
        {
            exit = true;
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Hall of fame
            try
            {
                Form3 form3 = new Form3();
                form3.Form_2 = this;
                this.Hide();
                form3.Show();
            }
            catch (Exception)
            {
                return;
            }

        }

        int counter = 0;
        int len = 0;
        string txt;

        private void timer1_Tick(object sender, EventArgs e)
        {

            counter++;
            if (counter > len)
            {
                counter = 0;
                label1.Text = "";


            }

            else
            {

                label1.Text = txt.Substring(0, counter);
                if (counter == txt.Length)
                    timer1.Stop();
            }
        }
        login f = new login();
        //click ........
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dangNhap();

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.ShowDialog();
            dangNhap();
            txt = label1.Text;
            len = txt.Length;
            label1.Text = "";
            timer1.Start();

        }
        private void dangNhap()
        {

            f.yourName = new login.GETDATA(getUName);
            f.ShowDialog();
        }

        public delegate void delPassData(string text);
        public delPassData name;
        private void button2_Click(object sender, EventArgs e)
        {
            FrmLevel frm = new FrmLevel();
            delPassData del = new delPassData(frm.data);
            frm.data(label2.Text);
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                tuto tutorialForm = new tuto();
                tutorialForm.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Chương trình bị lỗi ở phần Tập gõ văn bản!");
            }
        }
        public void getUName(string value)
        {
            label2.Text = value;
        }

    }

}