using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Yamb.Model;

namespace Yamb.App
{
    public partial class frmHighscoreDisplay : Form
    {
        public frmHighscoreDisplay()
        {
            InitializeComponent();
            PopulateScreen();
        }

        public frmHighscoreDisplay(int newRank)
        {
            InitializeComponent();
            Label rankName = GetLabel("lbl_rank" + newRank.ToString() + "Player");
            rankName.ForeColor = Color.Red;
            PopulateScreen();
        }

        private void PopulateScreen()
        {
            lbl_rank1Player.Text = Highscore.GetInstance().GetHighscore(1).Player;
            lbl_rank2Player.Text = Highscore.GetInstance().GetHighscore(2).Player;
            lbl_rank3Player.Text = Highscore.GetInstance().GetHighscore(3).Player;
            lbl_rank4Player.Text = Highscore.GetInstance().GetHighscore(4).Player;
            lbl_rank5Player.Text = Highscore.GetInstance().GetHighscore(5).Player;

            lbl_rank1Result.Text = Highscore.GetInstance().GetHighscore(1).Result.ToString();
            lbl_rank2Result.Text = Highscore.GetInstance().GetHighscore(2).Result.ToString();
            lbl_rank3Result.Text = Highscore.GetInstance().GetHighscore(3).Result.ToString();
            lbl_rank4Result.Text = Highscore.GetInstance().GetHighscore(4).Result.ToString();
            lbl_rank5Result.Text = Highscore.GetInstance().GetHighscore(5).Result.ToString();
        }

        private Label GetLabel(string labelName)
        {
            foreach (Control control in Controls)
            {
                if (control is Label && control.Name == labelName)
                {
                    return (Label)control;
                }
            }
            throw new Exception();
        }

        private void btn_displayTable1_Click(object sender, EventArgs e)
        {
            frmBestHighscore bestHighscore = new frmBestHighscore(1);
            bestHighscore.Show();
        }

        private void btn_displayTable2_Click(object sender, EventArgs e)
        {
            frmBestHighscore bestHighscore = new frmBestHighscore(2);
            bestHighscore.Show();
        }

        private void btn_displayTable3_Click(object sender, EventArgs e)
        {
            frmBestHighscore bestHighscore = new frmBestHighscore(3);
            bestHighscore.Show();
        }

        private void btn_displayTable4_Click(object sender, EventArgs e)
        {
            frmBestHighscore bestHighscore = new frmBestHighscore(4);
            bestHighscore.Show();
        }

        private void btn_displayTable5_Click(object sender, EventArgs e)
        {
            frmBestHighscore bestHighscore = new frmBestHighscore(5);
            bestHighscore.Show();
        }
    }
}
