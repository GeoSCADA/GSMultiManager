namespace GSMultiManagerConfig
{
	partial class MainForm
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
			this.btnClose = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.lstServers = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnRemove = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUser = new System.Windows.Forms.TextBox();
			this.txtServer = new System.Windows.Forms.TextBox();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.lblStatus = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(355, 332);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(98, 32);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(355, 137);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(98, 32);
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "Add";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// lstServers
			// 
			this.lstServers.FormattingEnabled = true;
			this.lstServers.ItemHeight = 16;
			this.lstServers.Location = new System.Drawing.Point(32, 50);
			this.lstServers.Name = "lstServers";
			this.lstServers.Size = new System.Drawing.Size(189, 276);
			this.lstServers.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(29, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(61, 17);
			this.label1.TabIndex = 3;
			this.label1.Text = "Systems";
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(32, 332);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(98, 32);
			this.btnRemove.TabIndex = 4;
			this.btnRemove.Text = "Remove";
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(266, 109);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(69, 17);
			this.label3.TabIndex = 14;
			this.label3.Text = "Password";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(266, 81);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 17);
			this.label2.TabIndex = 13;
			this.label2.Text = "User name";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(266, 53);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(54, 17);
			this.label4.TabIndex = 12;
			this.label4.Text = "System";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(355, 109);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(100, 22);
			this.txtPassword.TabIndex = 11;
			// 
			// txtUser
			// 
			this.txtUser.Location = new System.Drawing.Point(355, 81);
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new System.Drawing.Size(100, 22);
			this.txtUser.TabIndex = 10;
			// 
			// txtServer
			// 
			this.txtServer.Location = new System.Drawing.Point(355, 53);
			this.txtServer.Name = "txtServer";
			this.txtServer.Size = new System.Drawing.Size(100, 22);
			this.txtServer.TabIndex = 9;
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(251, 250);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(98, 32);
			this.btnStart.TabIndex = 15;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(355, 250);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(98, 32);
			this.btnStop.TabIndex = 16;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(248, 221);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(158, 17);
			this.label5.TabIndex = 17;
			this.label5.Text = "Monitor Service Status: ";
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(402, 221);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(70, 17);
			this.lblStatus.TabIndex = 18;
			this.lblStatus.Text = "Unknown ";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(534, 423);
			this.ControlBox = false;
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtUser);
			this.Controls.Add(this.txtServer);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstServers);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnClose);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "GS Multi Manager Setup";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.ListBox lstServers;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtUser;
		private System.Windows.Forms.TextBox txtServer;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lblStatus;
	}
}

