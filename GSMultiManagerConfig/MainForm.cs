using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;

namespace GSMultiManagerConfig
{
	// Settings form
	public partial class MainForm : Form
	{
		public System.Timers.Timer timer;
		public ServiceController myService;

		public MainForm()
		{
			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;

			var SystemList = Program.RegReadSystemNames();
			foreach (string valueName in SystemList)
			{
				lstServers.Items.Add(valueName);
			}

			// Service
			myService = new ServiceController();
			myService.ServiceName = "GSMultiManagerSvc";

			//Start status timer
			timer = new System.Timers.Timer();
			timer.Interval = 5000;  // 5 sec
			timer.AutoReset = true; // request rerun when ready
			timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
			timer.Start();
		}


		private void btnClose_Click(object sender, EventArgs e)
		{
			// Hide form
			this.Visible = false;
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			// Check an item has been selected
			if (lstServers.SelectedIndex < 0)
			{
				// None selected
				return;
			}
			var serverToRemove = lstServers.Text;
			// Confirm with modal dialog
			var r = MessageBox.Show("Delete system '" + serverToRemove + "'?", "GS Multi Manager Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (r == 0)
			{
				return;
			}
			// Remove from registry and list
			lstServers.Items.Remove(serverToRemove);
			Program.RegDeleteSystem(serverToRemove);
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			txtServer.Text = txtServer.Text.Trim();
			// Validation
			if (txtServer.Text.Length == 0)
			{
				MessageBox.Show("System name is empty", "GS Multi Manager Setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (txtUser.Text.Length == 0)
			{
				MessageBox.Show("User name is empty", "GS Multi Manager Setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (txtPassword.Text.Length == 0)
			{
				MessageBox.Show("Password is empty", "GS Multi Manager Setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			// Check duplicates
			foreach (string servername in lstServers.Items)
			{
				if (servername == txtServer.Text)
				{
					MessageBox.Show("Duplicate system name, please remove original first.", "GS Multi Manager Setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}
			// Add server to list
			lstServers.Items.Add(txtServer.Text);
			// Encrypt and Write to registry
			Program.EncryptCreds(txtUser.Text, txtPassword.Text, out string EncUser, out string EncPassword);

			Program.RegSetSystem(txtServer.Text, EncUser + "/" + EncPassword);
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			myService.Refresh();
			if (myService.Status == ServiceControllerStatus.Stopped)
			{
				try
				{
					myService.Start();
				}
				catch
				{
					MessageBox.Show("Cannot start service.", "GS Multi Manager Setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}


		public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
		{
			myService.Refresh();
			try
			{
				ServiceControllerStatus s = myService.Status;
				this.Controls["lblStatus"].Text = s.ToString();
			}
			catch (System.InvalidOperationException) // No service found
			{
				this.Controls["lblStatus"].Text = "No Service";
			}
			Application.DoEvents();
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			myService.Refresh();
			if (myService.Status == ServiceControllerStatus.Running)
			{
				try
				{
					myService.Stop();
				}
				catch
				{
					MessageBox.Show("Cannot stop service.", "GS Multi Manager Setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.BringToFront();
			this.TopMost = true;
			this.Focus();
		}
	}
}
