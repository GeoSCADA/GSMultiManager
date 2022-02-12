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
	public partial class ResetPassword : Form
	{
		static Form mform = null;
		public System.Timers.Timer timer;

		public ResetPassword()
		{
			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;

			var SystemList = Program.RegReadSystemNames();
			foreach (string valueName in SystemList)
			{
				lstServers.Items.Add(valueName);
			}
			var UserList = Program.FileReadUserNames();
			foreach (UserEntry user in UserList)
			{
				lstUsers.Items.Add(user.Name);
			}

			//Start status timer
			timer = new System.Timers.Timer();
			timer.Interval = 5000;  // 5 sec
			timer.AutoReset = true; // request rerun when ready
			timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
			timer.Start();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void btnSettings_Click(object sender, EventArgs e)
		{
			if (mform == null)
			{
				mform = new MainForm();
			}
			mform.Show();
		}

		private void btnChange_Click(object sender, EventArgs e)
		{
			if (txtUser.Text == "")
			{
				MessageBox.Show("User name is blank", "Change Password");
				return;
			}
			if (txtPassword.Text == "")
			{
				MessageBox.Show("Password is blank", "Change Password");
				return;
			}
			// Add user, password and a list of systems to the user list
			lstUsers.Items.Add(txtUser.Text);

			// Encrypt and Write to registry
			UserEntry user = new UserEntry( txtUser.Text);
			Program.EncryptCreds(txtUser.Text, txtPassword.Text, out user.EncryptedName, out user.EncryptedPassword);
			foreach ( var system in lstServers.Items)
			{
				user.SystemsToChange.Add( (string)system);
			}
			// Add whether to enable or disable account
			user.Enabled = chkEnable.Checked;
			Program.FileSetUser( user );
		}

		private void ResetPassword_Load(object sender, EventArgs e)
		{
			// If no systems then open config form
			var SystemList = Program.RegReadSystemNames();
			if (SystemList.Count == 0)
			{
				if (mform == null)
				{
					mform = new MainForm();
				}
				mform.Show();
				this.SendToBack();
			}
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			// Check an item has been selected
			if (lstUsers.SelectedIndex < 0)
			{
				// None selected
				return;
			}
			var userToRemove = lstUsers.Text;
			// Confirm with modal dialog
			var r = MessageBox.Show("Remove user '" + userToRemove + "' from update list?", "GS Multi Manager Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (r == 0)
			{
				return;
			}
			// Remove from registry and list
			lstUsers.Items.Remove(userToRemove);
			Program.FileDeleteUser(userToRemove);
		}

		public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
		{
			RefreshList();
		}

		public bool RefreshingUsers = false;
		public void RefreshList()
		{
			RefreshingUsers = true;
			// Refresh list of users on timer expiry or when user is clicked
			var UserList = Program.FileReadUserNames();
			// As this refreshes and interferes with the Remove button, check selection and reselect it after
			int SelectedUser = lstUsers.SelectedIndex;
			string SelectedUserName = "";
			if (SelectedUser >= 0)
			{
				SelectedUserName = (string)lstUsers.Items[SelectedUser];
			}
			// Clear and re-add
			lstUsers.Items.Clear();
			foreach (UserEntry user in UserList)
			{
				lstUsers.Items.Add(user.Name);
				// Reselect
				if (user.Name == SelectedUserName)
				{
					lstUsers.SelectedIndex = lstUsers.Items.Count - 1;
				}
			}
			List<string> SystemsToChange = new List<string>();
			if (SelectedUserName != "")
			{
				IEnumerable<UserEntry> ThisUserList = from u in UserList where u.Name == SelectedUserName select u;
				if (ThisUserList.Count() == 1)
				{
					UserEntry ThisUser = ThisUserList.First();
					SystemsToChange = ThisUser.SystemsToChange;
				}
			}
			// Servers
			RefreshServers(SystemsToChange);

			Application.DoEvents();
			RefreshingUsers = false;
		}

		// Refresh server list 
		private void RefreshServers( List<String> ServerList)
		{
			lstServers.Items.Clear();
			var SystemList = Program.RegReadSystemNames();
			foreach (string valueName in SystemList)
			{
				if ((ServerList).Contains(valueName))
				{
					lstServers.Items.Add(valueName + " - Pending");
				}
				else
				{
					// Either done or no user is selected
					lstServers.Items.Add(valueName);
				}
			}
		}

		private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!RefreshingUsers)
			{
				RefreshList();
			}
		}
	}
}
