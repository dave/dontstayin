namespace ChatLoadTester
{
	partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.txtServerAddress = new System.Windows.Forms.TextBox();
            this.lblLocalServer = new System.Windows.Forms.Label();
            this.btnGetRandomServer = new System.Windows.Forms.Button();
            this.numUsers = new System.Windows.Forms.NumericUpDown();
            this.numRooms = new System.Windows.Forms.NumericUpDown();
            this.numPostFrequency = new System.Windows.Forms.NumericUpDown();
            this.chkNoPosts = new System.Windows.Forms.CheckBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.numRequestFrequency = new System.Windows.Forms.NumericUpDown();
            this.numRequestorUsers = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnShowUserDetails = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRooms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPostFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRequestFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRequestorUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server to connect to";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Number of Posters";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Number of Rooms";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Frequency of posts";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(191, 22);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(51, 17);
            this.radioButton1.TabIndex = 6;
            this.radioButton1.Text = "Local";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(191, 45);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(65, 17);
            this.radioButton2.TabIndex = 7;
            this.radioButton2.Text = "Random";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(191, 68);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(60, 17);
            this.radioButton3.TabIndex = 8;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Specify";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // txtServerAddress
            // 
            this.txtServerAddress.Enabled = false;
            this.txtServerAddress.Location = new System.Drawing.Point(272, 68);
            this.txtServerAddress.Name = "txtServerAddress";
            this.txtServerAddress.Size = new System.Drawing.Size(152, 20);
            this.txtServerAddress.TabIndex = 9;
            this.txtServerAddress.Text = "192.168.113.131:9000";
            // 
            // lblLocalServer
            // 
            this.lblLocalServer.AutoSize = true;
            this.lblLocalServer.Location = new System.Drawing.Point(272, 25);
            this.lblLocalServer.Name = "lblLocalServer";
            this.lblLocalServer.Size = new System.Drawing.Size(0, 13);
            this.lblLocalServer.TabIndex = 10;
            // 
            // btnGetRandomServer
            // 
            this.btnGetRandomServer.Location = new System.Drawing.Point(272, 41);
            this.btnGetRandomServer.Name = "btnGetRandomServer";
            this.btnGetRandomServer.Size = new System.Drawing.Size(75, 23);
            this.btnGetRandomServer.TabIndex = 11;
            this.btnGetRandomServer.Text = "Randomize";
            this.btnGetRandomServer.UseVisualStyleBackColor = true;
            // 
            // numUsers
            // 
            this.numUsers.Location = new System.Drawing.Point(191, 178);
            this.numUsers.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUsers.Name = "numUsers";
            this.numUsers.Size = new System.Drawing.Size(73, 20);
            this.numUsers.TabIndex = 12;
            this.numUsers.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numUsers.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // numRooms
            // 
            this.numRooms.Location = new System.Drawing.Point(191, 128);
            this.numRooms.Name = "numRooms";
            this.numRooms.Size = new System.Drawing.Size(72, 20);
            this.numRooms.TabIndex = 13;
            this.numRooms.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numRooms.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // numPostFrequency
            // 
            this.numPostFrequency.Location = new System.Drawing.Point(191, 226);
            this.numPostFrequency.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numPostFrequency.Name = "numPostFrequency";
            this.numPostFrequency.Size = new System.Drawing.Size(72, 20);
            this.numPostFrequency.TabIndex = 14;
            this.numPostFrequency.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numPostFrequency.ValueChanged += new System.EventHandler(this.numPostFrequency_ValueChanged);
            // 
            // chkNoPosts
            // 
            this.chkNoPosts.AutoSize = true;
            this.chkNoPosts.Location = new System.Drawing.Point(275, 226);
            this.chkNoPosts.Name = "chkNoPosts";
            this.chkNoPosts.Size = new System.Drawing.Size(83, 17);
            this.chkNoPosts.TabIndex = 15;
            this.chkNoPosts.Text = "Never posts";
            this.chkNoPosts.UseVisualStyleBackColor = true;
            this.chkNoPosts.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(191, 91);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(51, 17);
            this.radioButton4.TabIndex = 16;
            this.radioButton4.Text = "Cycle";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 286);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Total requests";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(191, 283);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(72, 20);
            this.textBox1.TabIndex = 18;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(192, 333);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(71, 20);
            this.textBox2.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(55, 336);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Total posts";
            // 
            // btnGo
            // 
            this.btnGo.BackColor = System.Drawing.Color.Lime;
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Location = new System.Drawing.Point(635, 164);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(93, 82);
            this.btnGo.TabIndex = 21;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.button1_Click);
            // 
            // numRequestFrequency
            // 
            this.numRequestFrequency.Location = new System.Drawing.Point(521, 224);
            this.numRequestFrequency.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numRequestFrequency.Name = "numRequestFrequency";
            this.numRequestFrequency.Size = new System.Drawing.Size(72, 20);
            this.numRequestFrequency.TabIndex = 25;
            this.numRequestFrequency.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numRequestFrequency.ValueChanged += new System.EventHandler(this.numRequestFrequency_ValueChanged);
            // 
            // numRequestorUsers
            // 
            this.numRequestorUsers.Location = new System.Drawing.Point(521, 176);
            this.numRequestorUsers.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numRequestorUsers.Name = "numRequestorUsers";
            this.numRequestorUsers.Size = new System.Drawing.Size(73, 20);
            this.numRequestorUsers.TabIndex = 24;
            this.numRequestorUsers.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numRequestorUsers.ValueChanged += new System.EventHandler(this.numRequestorUsers_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(384, 224);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Frequency of requests";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(384, 178);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Number of Requestors";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(275, 283);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(453, 226);
            this.richTextBox1.TabIndex = 26;
            this.richTextBox1.Text = "";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(58, 389);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(204, 21);
            this.comboBox1.TabIndex = 27;
            // 
            // btnShowUserDetails
            // 
            this.btnShowUserDetails.Location = new System.Drawing.Point(58, 437);
            this.btnShowUserDetails.Name = "btnShowUserDetails";
            this.btnShowUserDetails.Size = new System.Drawing.Size(203, 23);
            this.btnShowUserDetails.TabIndex = 28;
            this.btnShowUserDetails.Text = "Show User details";
            this.btnShowUserDetails.UseVisualStyleBackColor = true;
            this.btnShowUserDetails.Click += new System.EventHandler(this.btnShowUserDetails_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 521);
            this.Controls.Add(this.btnShowUserDetails);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.numRequestFrequency);
            this.Controls.Add(this.numRequestorUsers);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.radioButton4);
            this.Controls.Add(this.chkNoPosts);
            this.Controls.Add(this.numPostFrequency);
            this.Controls.Add(this.numRooms);
            this.Controls.Add(this.numUsers);
            this.Controls.Add(this.btnGetRandomServer);
            this.Controls.Add(this.lblLocalServer);
            this.Controls.Add(this.txtServerAddress);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Chat Server Load Tester";
            ((System.ComponentModel.ISupportInitialize)(this.numUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRooms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPostFrequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRequestFrequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRequestorUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton radioButton3;
		private System.Windows.Forms.TextBox txtServerAddress;
		private System.Windows.Forms.Label lblLocalServer;
		private System.Windows.Forms.Button btnGetRandomServer;
		private System.Windows.Forms.NumericUpDown numUsers;
		private System.Windows.Forms.NumericUpDown numRooms;
		private System.Windows.Forms.NumericUpDown numPostFrequency;
		private System.Windows.Forms.CheckBox chkNoPosts;
		private System.Windows.Forms.RadioButton radioButton4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.NumericUpDown numRequestFrequency;
		private System.Windows.Forms.NumericUpDown numRequestorUsers;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnShowUserDetails;
	}
}

