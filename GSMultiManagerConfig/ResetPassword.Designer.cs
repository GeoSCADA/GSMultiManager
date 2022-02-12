
namespace GSMultiManagerConfig
{
	partial class ResetPassword
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
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUser = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lstServers = new System.Windows.Forms.ListBox();
			this.btnChange = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnSettings = new System.Windows.Forms.Button();
			this.lstUsers = new System.Windows.Forms.ListBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btnRemove = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.chkEnable = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(25, 73);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(69, 17);
			this.label3.TabIndex = 22;
			this.label3.Text = "Password";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(25, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 17);
			this.label2.TabIndex = 21;
			this.label2.Text = "User name";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(114, 73);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(100, 22);
			this.txtPassword.TabIndex = 20;
			// 
			// txtUser
			// 
			this.txtUser.Location = new System.Drawing.Point(114, 45);
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new System.Drawing.Size(100, 22);
			this.txtUser.TabIndex = 19;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(449, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(61, 17);
			this.label1.TabIndex = 18;
			this.label1.Text = "Systems";
			// 
			// lstServers
			// 
			this.lstServers.FormattingEnabled = true;
			this.lstServers.ItemHeight = 16;
			this.lstServers.Location = new System.Drawing.Point(452, 46);
			this.lstServers.Name = "lstServers";
			this.lstServers.Size = new System.Drawing.Size(189, 340);
			this.lstServers.TabIndex = 17;
			// 
			// btnChange
			// 
			this.btnChange.Location = new System.Drawing.Point(114, 133);
			this.btnChange.Name = "btnChange";
			this.btnChange.Size = new System.Drawing.Size(98, 32);
			this.btnChange.TabIndex = 16;
			this.btnChange.Text = "Change";
			this.btnChange.UseVisualStyleBackColor = true;
			this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(10, 412);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(98, 32);
			this.btnClose.TabIndex = 15;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnSettings
			// 
			this.btnSettings.Location = new System.Drawing.Point(114, 412);
			this.btnSettings.Name = "btnSettings";
			this.btnSettings.Size = new System.Drawing.Size(98, 32);
			this.btnSettings.TabIndex = 23;
			this.btnSettings.Text = "Settings";
			this.btnSettings.UseVisualStyleBackColor = true;
			this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
			// 
			// lstUsers
			// 
			this.lstUsers.FormattingEnabled = true;
			this.lstUsers.ItemHeight = 16;
			this.lstUsers.Location = new System.Drawing.Point(249, 46);
			this.lstUsers.Name = "lstUsers";
			this.lstUsers.Size = new System.Drawing.Size(176, 340);
			this.lstUsers.TabIndex = 24;
			this.lstUsers.SelectedIndexChanged += new System.EventHandler(this.lstUsers_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(246, 18);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(101, 17);
			this.label4.TabIndex = 25;
			this.label4.Text = "Users Pending";
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(249, 392);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(98, 32);
			this.btnRemove.TabIndex = 27;
			this.btnRemove.Text = "Remove";
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(25, 18);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(123, 17);
			this.label5.TabIndex = 28;
			this.label5.Text = "Enter User Details";
			// 
			// chkEnable
			// 
			this.chkEnable.AutoSize = true;
			this.chkEnable.Checked = true;
			this.chkEnable.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkEnable.Location = new System.Drawing.Point(114, 101);
			this.chkEnable.Name = "chkEnable";
			this.chkEnable.Size = new System.Drawing.Size(93, 26);
			this.chkEnable.TabIndex = 29;
			this.chkEnable.Text = "Enable";
			this.chkEnable.UseVisualStyleBackColor = true;
			// 
			// ResetPassword
			// 
			this.AcceptButton = this.btnChange;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(661, 456);
			this.ControlBox = false;
			this.Controls.Add(this.chkEnable);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.lstUsers);
			this.Controls.Add(this.btnSettings);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtUser);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstServers);
			this.Controls.Add(this.btnChange);
			this.Controls.Add(this.btnClose);
			this.Name = "ResetPassword";
			this.Text = "ResetPassword";
			this.Load += new System.EventHandler(this.ResetPassword_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtUser;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox lstServers;
		private System.Windows.Forms.Button btnChange;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnSettings;
		private System.Windows.Forms.ListBox lstUsers;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox chkEnable;
	}
}