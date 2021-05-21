namespace VoiceAuthomatedEmail
{
    partial class login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.txtUsr = new System.Windows.Forms.TextBox();
            this.txtPin = new System.Windows.Forms.TextBox();
            this.lblUsr = new System.Windows.Forms.Label();
            this.lblPin = new System.Windows.Forms.Label();
            this.btnLgIn = new System.Windows.Forms.Button();
            this.btnReg = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUsrMnl = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUsr
            // 
            this.txtUsr.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsr.Location = new System.Drawing.Point(206, 73);
            this.txtUsr.Name = "txtUsr";
            this.txtUsr.Size = new System.Drawing.Size(299, 36);
            this.txtUsr.TabIndex = 0;
            // 
            // txtPin
            // 
            this.txtPin.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPin.Location = new System.Drawing.Point(206, 167);
            this.txtPin.Name = "txtPin";
            this.txtPin.PasswordChar = '*';
            this.txtPin.Size = new System.Drawing.Size(299, 36);
            this.txtPin.TabIndex = 1;
            // 
            // lblUsr
            // 
            this.lblUsr.AutoSize = true;
            this.lblUsr.BackColor = System.Drawing.Color.Transparent;
            this.lblUsr.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsr.ForeColor = System.Drawing.Color.Black;
            this.lblUsr.Location = new System.Drawing.Point(116, 76);
            this.lblUsr.Name = "lblUsr";
            this.lblUsr.Size = new System.Drawing.Size(60, 29);
            this.lblUsr.TabIndex = 2;
            this.lblUsr.Text = "User";
            // 
            // lblPin
            // 
            this.lblPin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPin.AutoSize = true;
            this.lblPin.BackColor = System.Drawing.Color.Transparent;
            this.lblPin.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPin.ForeColor = System.Drawing.Color.Black;
            this.lblPin.Location = new System.Drawing.Point(126, 167);
            this.lblPin.Name = "lblPin";
            this.lblPin.Size = new System.Drawing.Size(45, 29);
            this.lblPin.TabIndex = 3;
            this.lblPin.Text = "Pin";
            // 
            // btnLgIn
            // 
            this.btnLgIn.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnLgIn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLgIn.BackgroundImage")));
            this.btnLgIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLgIn.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLgIn.Location = new System.Drawing.Point(431, 356);
            this.btnLgIn.Name = "btnLgIn";
            this.btnLgIn.Size = new System.Drawing.Size(126, 50);
            this.btnLgIn.TabIndex = 4;
            this.btnLgIn.Text = "Log In";
            this.btnLgIn.UseVisualStyleBackColor = false;
            this.btnLgIn.Click += new System.EventHandler(this.btnLgIn_Click);
            // 
            // btnReg
            // 
            this.btnReg.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnReg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReg.BackgroundImage")));
            this.btnReg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReg.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReg.Location = new System.Drawing.Point(108, 47);
            this.btnReg.Name = "btnReg";
            this.btnReg.Size = new System.Drawing.Size(219, 62);
            this.btnReg.TabIndex = 5;
            this.btnReg.Text = "Register";
            this.btnReg.UseVisualStyleBackColor = false;
            this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.OldLace;
            this.panel1.BackgroundImage = global::VoiceAuthomatedEmail.Properties.Resources.bgclr;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblUsr);
            this.panel1.Controls.Add(this.txtUsr);
            this.panel1.Controls.Add(this.btnLgIn);
            this.panel1.Controls.Add(this.lblPin);
            this.panel1.Controls.Add(this.txtPin);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(174, 193);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(604, 444);
            this.panel1.TabIndex = 6;
            // 
            // btnUsrMnl
            // 
            this.btnUsrMnl.BackColor = System.Drawing.Color.Transparent;
            this.btnUsrMnl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUsrMnl.BackgroundImage")));
            this.btnUsrMnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUsrMnl.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsrMnl.Image = ((System.Drawing.Image)(resources.GetObject("btnUsrMnl.Image")));
            this.btnUsrMnl.Location = new System.Drawing.Point(108, 167);
            this.btnUsrMnl.Name = "btnUsrMnl";
            this.btnUsrMnl.Size = new System.Drawing.Size(211, 181);
            this.btnUsrMnl.TabIndex = 7;
            this.btnUsrMnl.Text = "User Manual";
            this.btnUsrMnl.UseVisualStyleBackColor = false;
            this.btnUsrMnl.Click += new System.EventHandler(this.btnUsrMnl_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.OldLace;
            this.panel2.BackgroundImage = global::VoiceAuthomatedEmail.Properties.Resources.bgclr;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnReg);
            this.panel2.Controls.Add(this.btnUsrMnl);
            this.panel2.Location = new System.Drawing.Point(914, 197);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(417, 454);
            this.panel2.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(692, 952);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(496, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "Copyright © All Rights Reserved VBE MicroLink College";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(1379, 96);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(507, 741);
            this.panel3.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(190, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "Voice Based email";
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BackgroundImage = global::VoiceAuthomatedEmail.Properties.Resources._WallpapersGram4k___Durdle_Door;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1924, 1018);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "login";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Log In";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsr;
        private System.Windows.Forms.TextBox txtPin;
        private System.Windows.Forms.Label lblUsr;
        private System.Windows.Forms.Label lblPin;
        private System.Windows.Forms.Button btnLgIn;
        private System.Windows.Forms.Button btnReg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnUsrMnl;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
    }
}

