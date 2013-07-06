using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yamb.App
{
    public partial class frmWelcomeScreen : Form
    {
        public string Player;

        public frmWelcomeScreen()
        {
            InitializeComponent();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (txt_Player.Text == "")
            {
                frmMsgBox mbox = new frmMsgBox();
                mbox.ShowDialog();
                DialogResult = DialogResult.None;
            }
            Player = txt_Player.Text;
        }

    }
}
