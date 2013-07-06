namespace Yamb.App
{
    partial class frmConfirm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_msg = new System.Windows.Forms.Label();
            this.bt_da = new System.Windows.Forms.Button();
            this.btn_ne = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_msg
            // 
            this.lbl_msg.AutoSize = true;
            this.lbl_msg.Location = new System.Drawing.Point(12, 9);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Size = new System.Drawing.Size(0, 13);
            this.lbl_msg.TabIndex = 0;
            // 
            // bt_da
            // 
            this.bt_da.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bt_da.Location = new System.Drawing.Point(12, 38);
            this.bt_da.Name = "bt_da";
            this.bt_da.Size = new System.Drawing.Size(49, 23);
            this.bt_da.TabIndex = 1;
            this.bt_da.Text = "Da";
            this.bt_da.UseVisualStyleBackColor = true;
            // 
            // btn_ne
            // 
            this.btn_ne.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ne.Location = new System.Drawing.Point(70, 38);
            this.btn_ne.Name = "btn_ne";
            this.btn_ne.Size = new System.Drawing.Size(49, 23);
            this.btn_ne.TabIndex = 2;
            this.btn_ne.Text = "Ne";
            this.btn_ne.UseVisualStyleBackColor = true;
            // 
            // frmConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_ne;
            this.ClientSize = new System.Drawing.Size(131, 72);
            this.ControlBox = false;
            this.Controls.Add(this.btn_ne);
            this.Controls.Add(this.bt_da);
            this.Controls.Add(this.lbl_msg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfirm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_msg;
        private System.Windows.Forms.Button bt_da;
        private System.Windows.Forms.Button btn_ne;
    }
}