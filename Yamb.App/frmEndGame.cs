using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yamb
{
    public partial class frmEndGame : Form
    {
        public frmEndGame()
        {
            InitializeComponent();
        }

        public bool ShowPoints(int totalPoints)
        {
            lbl_msg1.Text += totalPoints.ToString();
            DialogResult res = this.ShowDialog();

            if (res == DialogResult.OK)
                return true;
            else
            {
                return false;
            }
        }
    }
}
