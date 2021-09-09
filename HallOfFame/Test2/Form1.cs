using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.Windows.Forms;


namespace Main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }


        int x = 0;

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            x++;

            if (x == 1)
            {
                label1.Show();
                label14.Show();
            }
            if (x == 2)
            {
                label2.Show();
            }
            if (x == 3)
            {
                label3.Show();
            }
            if (x == 4)
            {
                label4.Show();
            }
            if (x == 5)
            {
                label5.Show();
            }
            if (x == 6)
            {
                label6.Show();
            }

            if (x == 7)
            {
                label7.Show();
                timer1.Interval = 200;
            }
            if (x == 8)
            {
                label8.Show();
            }
            if (x == 9)
            {
                label9.Show();
            }
            if (x == 10)
            {
                label10.Show();
            }
            if (x == 11)
            {
                label11.Show();
            }
            if (x == 12)
            {
                label12.Show();
                timer1.Interval = 500;
            }
            if (x == 13)
            {
                label13.Show();
            }

            if (x == 13)
            {
                timer1.Stop();
                this.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txt = label15.Text;
            len = txt.Length;
            label15.Text = "";
            timer2.Start();
            label1.Hide();
            label2.Hide(); label3.Hide(); label4.Hide(); label5.Hide(); label6.Hide();
            label7.Hide(); label8.Hide(); label9.Hide(); label10.Hide(); label11.Hide();
            label12.Hide(); label13.Hide();
            timer1.Interval = 500;
            timer1.Start();

        }
        int counter = 0;
        int len = 0;
        string txt;

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            counter++;
            if (counter > len)
            {
                counter = 0;
                label15.Text = "";
            }

            else
            {

                label15.Text = txt.Substring(0, counter);
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
    }
}

