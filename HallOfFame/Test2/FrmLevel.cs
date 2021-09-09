using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using game;

namespace Main
{
    public partial class FrmLevel : Form
    {
        public FrmLevel()
        {
            InitializeComponent();
        }

        public static string playerName;

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btLevelEasy_Click(object sender, EventArgs e)
        {
            Easy game = new Easy(playerName);
            game.ShowDialog();
        }

        private void btLevelNormal_Click(object sender, EventArgs e)
        {
            Normal game = new Normal(playerName);
            game.ShowDialog();
        }

        private void btLevelHard_Click(object sender, EventArgs e)
        {
            Hard game = new Hard(playerName);
            game.ShowDialog();
        }
        public void data(string name)
        {
            playerName = name;
        }
    }
}
