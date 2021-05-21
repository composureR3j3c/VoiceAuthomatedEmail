namespace VoiceAuthomatedEmail
{
    partial class received
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(received));
            this.btnRead = new System.Windows.Forms.Button();
            this.richTextBoxBody = new System.Windows.Forms.RichTextBox();
            this.dataGridViewRec = new System.Windows.Forms.DataGridView();
            this.Sender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateTim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReadout = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtFwdEml = new System.Windows.Forms.TextBox();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRec)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRead
            // 
            this.btnRead.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnRead.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRead.BackgroundImage")));
            this.btnRead.Enabled = false;
            this.btnRead.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRead.Location = new System.Drawing.Point(84, 877);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(151, 54);
            this.btnRead.TabIndex = 1;
            this.btnRead.Text = "Refresh";
            this.btnRead.UseVisualStyleBackColor = false;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // richTextBoxBody
            // 
            this.richTextBoxBody.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxBody.BackColor = System.Drawing.Color.WhiteSmoke;
            this.richTextBoxBody.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxBody.Location = new System.Drawing.Point(741, 150);
            this.richTextBoxBody.Name = "richTextBoxBody";
            this.richTextBoxBody.Size = new System.Drawing.Size(1018, 671);
            this.richTextBoxBody.TabIndex = 2;
            this.richTextBoxBody.Text = "";
            // 
            // dataGridViewRec
            // 
            this.dataGridViewRec.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewRec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRec.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sender,
            this.Subject,
            this.DateTim,
            this.id});
            this.dataGridViewRec.Location = new System.Drawing.Point(71, 150);
            this.dataGridViewRec.Name = "dataGridViewRec";
            this.dataGridViewRec.RowTemplate.Height = 24;
            this.dataGridViewRec.Size = new System.Drawing.Size(625, 621);
            this.dataGridViewRec.TabIndex = 3;
            this.dataGridViewRec.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewRec_CellClick);
            this.dataGridViewRec.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewRec_CellContentClick);
            // 
            // Sender
            // 
            this.Sender.DataPropertyName = "Sender";
            this.Sender.HeaderText = "Sender";
            this.Sender.Name = "Sender";
            // 
            // Subject
            // 
            this.Subject.DataPropertyName = "Subject";
            this.Subject.HeaderText = "Subject";
            this.Subject.Name = "Subject";
            // 
            // DateTim
            // 
            this.DateTim.DataPropertyName = "Date";
            this.DateTim.HeaderText = "Date &Time";
            this.DateTim.Name = "DateTim";
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // btnReadout
            // 
            this.btnReadout.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnReadout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReadout.BackgroundImage")));
            this.btnReadout.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReadout.Location = new System.Drawing.Point(259, 877);
            this.btnReadout.Name = "btnReadout";
            this.btnReadout.Size = new System.Drawing.Size(151, 54);
            this.btnReadout.TabIndex = 5;
            this.btnReadout.Text = "Read";
            this.btnReadout.UseVisualStyleBackColor = false;
            this.btnReadout.Click += new System.EventHandler(this.btnReadout_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(435, 877);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(151, 54);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtFwdEml
            // 
            this.txtFwdEml.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFwdEml.Location = new System.Drawing.Point(1359, 909);
            this.txtFwdEml.Name = "txtFwdEml";
            this.txtFwdEml.Size = new System.Drawing.Size(315, 32);
            this.txtFwdEml.TabIndex = 7;
            this.txtFwdEml.Visible = false;
            // 
            // btnForward
            // 
            this.btnForward.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnForward.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnForward.BackgroundImage")));
            this.btnForward.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForward.Location = new System.Drawing.Point(1114, 877);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(151, 54);
            this.btnForward.TabIndex = 8;
            this.btnForward.Text = "Forward";
            this.btnForward.UseVisualStyleBackColor = false;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.Transparent;
            this.btnSend.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSend.BackgroundImage")));
            this.btnSend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSend.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(1705, 877);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(54, 54);
            this.btnSend.TabIndex = 9;
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Visible = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(1354, 877);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 29);
            this.label1.TabIndex = 10;
            this.label1.Text = "Recipient";
            this.label1.Visible = false;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(793, 965);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(496, 25);
            this.label7.TabIndex = 22;
            this.label7.Text = "Copyright © All Rights Reserved VBE MicroLink College";
            // 
            // received
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::VoiceAuthomatedEmail.Properties.Resources._WallpapersGram4k___Durdle_Door;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1907, 999);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.txtFwdEml);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnReadout);
            this.Controls.Add(this.dataGridViewRec);
            this.Controls.Add(this.richTextBoxBody);
            this.Controls.Add(this.btnRead);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "received";
            this.Text = "received";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.received_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRec)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.RichTextBox richTextBoxBody;
        private System.Windows.Forms.DataGridView dataGridViewRec;
        private System.Windows.Forms.Button btnReadout;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtFwdEml;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sender;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subject;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTim;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
    }
}