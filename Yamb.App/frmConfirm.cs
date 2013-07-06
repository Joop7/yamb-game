using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Yamb.Controller;

namespace Yamb.App
{
    public partial class frmConfirm : Form, IConfirmForm
    {
        public frmConfirm()
        {
            Location = Cursor.Position;
            InitializeComponent();
        }

        public bool ShowMsg(string msg)
        {
            lbl_msg.Text = msg;
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
