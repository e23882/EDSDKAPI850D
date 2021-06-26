namespace CameraControl
{
    partial class RemoteCapture
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.availableShotsLabel2 = new CameraControl.AvailableShotsLabel();
            this.availableShotsLabel1 = new CameraControl.AvailableShotsLabel();
            this.iso1 = new CameraControl.IsoComboBox();
            this.av1 = new CameraControl.AvComboBox();
            this.tv1 = new CameraControl.TvComboBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 15);
            this.label2.TabIndex = 81;
            this.label2.Text = "快門(TV)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(340, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 82;
            this.label3.Text = "光圈(AV)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(183, 26);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 15);
            this.label4.TabIndex = 83;
            this.label4.Text = "增益(ISO)";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(33, 150);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(109, 25);
            this.textBox1.TabIndex = 84;
            this.textBox1.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 85;
            this.label1.Text = "測光值";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(183, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 86;
            this.label5.Text = "閃光燈";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(183, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 15);
            this.label6.TabIndex = 87;
            this.label6.Text = "OFF";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(340, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 15);
            this.label7.TabIndex = 88;
            this.label7.Text = "目前套用設定";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(345, 138);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 15);
            this.label8.TabIndex = 89;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(340, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(124, 15);
            this.label9.TabIndex = 91;
            this.label9.Text = "嘗試取得測光值...";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(33, 240);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(420, 19);
            this.checkBox1.TabIndex = 92;
            this.checkBox1.Text = "依照測光板測光值設定相機 / 依照介面測光值,忽視特別設定";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(33, 268);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 25);
            this.textBox2.TabIndex = 94;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(152, 268);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 95;
            this.button1.Text = "Set";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // availableShotsLabel2
            // 
            this.availableShotsLabel2.AutoSize = true;
            this.availableShotsLabel2.Location = new System.Drawing.Point(0, 0);
            this.availableShotsLabel2.Name = "availableShotsLabel2";
            this.availableShotsLabel2.Size = new System.Drawing.Size(127, 15);
            this.availableShotsLabel2.TabIndex = 93;
            this.availableShotsLabel2.Text = "availableShotsLabel2";
            // 
            // availableShotsLabel1
            // 
            this.availableShotsLabel1.AutoSize = true;
            this.availableShotsLabel1.Location = new System.Drawing.Point(0, 0);
            this.availableShotsLabel1.Name = "availableShotsLabel1";
            this.availableShotsLabel1.Size = new System.Drawing.Size(127, 15);
            this.availableShotsLabel1.TabIndex = 90;
            this.availableShotsLabel1.Text = "availableShotsLabel1";
            // 
            // iso1
            // 
            this.iso1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.iso1.Enabled = false;
            this.iso1.FormattingEnabled = true;
            this.iso1.Location = new System.Drawing.Point(186, 61);
            this.iso1.Margin = new System.Windows.Forms.Padding(4);
            this.iso1.Name = "iso1";
            this.iso1.Size = new System.Drawing.Size(109, 23);
            this.iso1.TabIndex = 3;
            // 
            // av1
            // 
            this.av1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.av1.Enabled = false;
            this.av1.FormattingEnabled = true;
            this.av1.Location = new System.Drawing.Point(343, 61);
            this.av1.Margin = new System.Windows.Forms.Padding(4);
            this.av1.Name = "av1";
            this.av1.Size = new System.Drawing.Size(109, 23);
            this.av1.TabIndex = 2;
            // 
            // tv1
            // 
            this.tv1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tv1.Enabled = false;
            this.tv1.FormattingEnabled = true;
            this.tv1.Location = new System.Drawing.Point(36, 61);
            this.tv1.Margin = new System.Windows.Forms.Padding(4);
            this.tv1.Name = "tv1";
            this.tv1.Size = new System.Drawing.Size(109, 23);
            this.tv1.TabIndex = 1;
            // 
            // RemoteCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 305);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.availableShotsLabel2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.availableShotsLabel1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.iso1);
            this.Controls.Add(this.av1);
            this.Controls.Add(this.tv1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "RemoteCapture";
            this.Text = "Camera Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RemoteCapture_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private TvComboBox tv1;
        private AvComboBox av1;
        private IsoComboBox iso1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private AvailableShotsLabel availableShotsLabel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBox1;
        private AvailableShotsLabel availableShotsLabel2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
    }
}