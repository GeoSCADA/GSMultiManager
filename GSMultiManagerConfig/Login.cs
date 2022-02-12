using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSMultiManagerConfig
{
	public partial class Login : Form
	{
		public Login()
		{
			InitializeComponent();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			bool loginOK = false;

			// Check credentials against registry contents
			var SystemList = Program.RegReadSystemNames();
			foreach (string valueName in SystemList)
			{
				string creds = Program.RegGetSystemCreds(valueName);
				if (Program.DecryptCreds(creds, out var user, out var password))
				{
					if (txtServer.Text.ToLower() == valueName.ToLower() &&
						txtUser.Text.ToLower() == user.ToLower() &&
						txtPassword.Text == password)
					{
						// Successful login
						loginOK = true;
					}
				}
			}
			// If no systems in the list then log in anyway
			if (SystemList.Count == 0)
			{
				loginOK = true;
			}
			// This then opens main form
			if (!loginOK)
			{
				// Login failure
				MessageBox.Show("Invalid credentials", "Can not log in");
			}
			else
			{
				this.Hide();
				var rpform = new ResetPassword();
				rpform.Show();
			}
		}
	}
}
