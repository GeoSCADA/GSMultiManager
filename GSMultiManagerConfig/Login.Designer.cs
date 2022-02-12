
namespace GSMultiManagerConfig
{
	partial class Login
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUser = new System.Windows.Forms.TextBox();
			this.txtServer = new System.Windows.Forms.TextBox();
			this.btnLogin = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(19, 81);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(69, 17);
			this.label3.TabIndex = 22;
			this.label3.Text = "Password";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(19, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 17);
			this.label2.TabIndex = 21;
			this.label2.Text = "User name";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(19, 25);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(54, 17);
			this.label4.TabIndex = 20;
			this.label4.Text = "System";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(138, 81);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(100, 22);
			this.txtPassword.TabIndex = 3;
			// 
			// txtUser
			// 
			this.txtUser.Location = new System.Drawing.Point(138, 53);
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new System.Drawing.Size(100, 22);
			this.txtUser.TabIndex = 2;
			// 
			// txtServer
			// 
			this.txtServer.Location = new System.Drawing.Point(138, 25);
			this.txtServer.Name = "txtServer";
			this.txtServer.Size = new System.Drawing.Size(100, 22);
			this.txtServer.TabIndex = 1;
			// 
			// btnLogin
			// 
			this.btnLogin.Location = new System.Drawing.Point(138, 162);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(98, 32);
			this.btnLogin.TabIndex = 4;
			this.btnLogin.Text = "Login";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(4, 162);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(98, 32);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// Login
			// 
			this.AcceptButton = this.btnLogin;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(295, 221);
			this.ControlBox = false;
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtUser);
			this.Controls.Add(this.txtServer);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.btnCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(355, 352);
			this.Name = "Login";
			this.Text = "Multi Password Manager Login";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtUser;
		private System.Windows.Forms.TextBox txtServer;
		private System.Windows.Forms.Button btnLogin;
		private System.Windows.Forms.Button btnCancel;
	}
}