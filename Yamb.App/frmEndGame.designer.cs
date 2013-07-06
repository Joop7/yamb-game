namespace Yamb
{
    partial class frmEndGame
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
            this.btn_da = new System.Windows.Forms.Button();
            this.btn_ne = new System.Windows.Forms.Button();
            this.lbl_msg1 = new System.Windows.Forms.Label();
            this.lbl_msg2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_da
            // 
            this.btn_da.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_da.Location = new System.Drawing.Point(12, 71);
            this.btn_da.Name = "btn_da";
            this.btn_da.Size = new System.Drawing.Size(75, 23);
            this.btn_da.TabIndex = 0;
            this.btn_da.Text = "Da";
            this.btn_da.UseVisualStyleBackColor = true;
            // 
            // btn_ne
            // 
            this.btn_ne.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ne.Location = new System.Drawing.Point(93, 71);
            this.btn_ne.Name = "btn_ne";
            this.btn_ne.Size = new System.Drawing.Size(75, 23);
            this.btn_ne.TabIndex = 1;
            this.btn_ne.Text = "Ne";
            this.btn_ne.UseVisualStyleBackColor = true;
            // 
            // lbl_msg1
            // 
            this.lbl_msg1.AutoSize = true;
            this.lbl_msg1.Location = new System.Drawing.Point(12, 9);
            this.lbl_msg1.Name = "lbl_msg1";
            this.lbl_msg1.Size = new System.Drawing.Size(128, 13);
            this.lbl_msg1.TabIndex = 2;
            this.lbl_msg1.Text = "Čestitamo! Vaš rezultat je ";
            // 
            // lbl_msg2
            // 
            this.lbl_msg2.AutoSize = true;
            this.lbl_msg2.Location = new System.Drawing.Point(35, 45);
            this.lbl_msg2.Name = "lbl_msg2";
            this.lbl_msg2.Size = new System.Drawing.Size(116, 13);
            this.lbl_msg2.TabIndex = 3;
            this.lbl_msg2.Text = "Želite li ponovno igrati?";
            // 
            // frmEndGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_ne;
            this.ClientSize = new System.Drawing.Size(187, 108);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_msg2);
            this.Controls.Add(this.lbl_msg1);
            this.Controls.Add(this.btn_ne);
            this.Controls.Add(this.btn_da);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEndGame";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_da;
        private System.Windows.Forms.Button btn_ne;
        private System.Windows.Forms.Label lbl_msg1;
        private System.Windows.Forms.Label lbl_msg2;
    }
}