using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Win32;
using ClearScada.Client;
using System.Xml;
using System.Security;
using Newtonsoft.Json;

//In folder:
//C:\Users\sesa170272\Documents\Visual Studio 2015\Projects\GSMultiManager\GSMultiManagerSvc\bin\Debug
//
//To Install as a Service:
//c:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe GSMultiManagerSvc.exe
//Then:
// net start GSMultiManager

namespace GSMultiManagerSvc
{
	public partial class GSMultiManager : ServiceBase
	{
		// Errors and startup status in the MS Event Viewer - "Applications and Services Log", click "GSMultiManager"
		private System.Diagnostics.EventLog LMeventLog;
		private int eventId = 1;
		private System.Timers.Timer timer;
		private bool firstTime = true;
		// We log change data in this file, creating a new file each month
		private readonly string filepath = "C:\\ProgramData\\Schneider Electric\\ClearSCADA\\GS Multi Manager\\";
		private readonly string usagefilename = "GSMultiManagerData";
		private readonly string usagefileext = ".txt";
		// User data stored in json files in this folder
		private readonly string usersfolder = "Users\\";

		internal void TestStartupAndStop(string[] args)
		{
			this.OnStart(args);
			Console.ReadLine();
			this.OnStop();
		}

		public GSMultiManager()
		{
			InitializeComponent();

			// Create custom event log
			LMeventLog = new System.Diagnostics.EventLog();
			if (!System.Diagnostics.EventLog.SourceExists("GSMultiManager"))
			{
				System.Diagnostics.EventLog.CreateEventSource(
					"GSMultiManager", "GSMultiManager");
			}
			LMeventLog.Source = "GSMultiManager";
			LMeventLog.Log = ""; // Must be the same as Source, or blank
		}

		protected override void OnStart(string[] args)
		{
			LMeventLog.WriteEntry("GS Multi Manager Service Started");
			// Set up a timer that triggers every minute.
			timer = new System.Timers.Timer();
			timer.Interval = 1000;  // 1 second
			timer.AutoReset = true; // request rerun when ready
			timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
			timer.Start();

			// Create output folder if it does not exist
			try
			{
				Directory.CreateDirectory(filepath);
			}
			catch (Exception e)
			{
				LMeventLog.WriteEntry("Error creating path: " + filepath  + ", " + e.Message, EventLogEntryType.Error, eventId++);
			}
		}

		public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
		{
			timer.Interval = 10000;  // Continue at 10 second intervals
			timer.Enabled = false;   // Stop timer until this function is complete (e.g. if takes over 10s)
			bool status = true;

			if (firstTime)
			{
				LMeventLog.WriteEntry("First scheduled operation", EventLogEntryType.Information, eventId++);
			}

			// Systems in the registry
			RegistryKey SystemKey;
			try
			{
				var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
				SystemKey = baseKey.CreateSubKey(@"SOFTWARE\Schneider Electric\GSMultiManager\Systems",
													RegistryKeyPermissionCheck.ReadSubTree);
			} catch (Exception e)
			{
				LMeventLog.WriteEntry("Cannot read servers from registry " + e.Message, EventLogEntryType.Information, eventId++);
				return;
			}

			var UserList = FileReadUserNames();

			// List all key names - the servers
			foreach (string server in SystemKey.GetValueNames())
			{
				string valueData = SystemKey.GetValue(server).ToString();
				// Key name is the server
				if (firstTime)
				{
					LMeventLog.WriteEntry("System: " + server, EventLogEntryType.Information, eventId++);
				}

				// No point in processing a server unless there are one or more users requiring password change for a server
				// List all key names and encrypted passwords - the users - one list for this system only
				var UserListToProcess = new List<UserEntry>();

				foreach (var user in UserList)
				{
					foreach (var system in user.SystemsToChange)
					{
						if (system == server)
						{
							UserListToProcess.Add(user);
						}
					}
				}
				if (UserListToProcess.Count > 0)
				{
					string SystemData = SystemKey.GetValue(server).ToString();
					if ( !ProcessSystem(server, SystemData, UserListToProcess))
					{
						status = false;
					}
				}
			}

			if (firstTime && !status)
			{
				LMeventLog.WriteEntry("At least one system failed operation", EventLogEntryType.Error, eventId++);
			}

			// Stop noisy event logging after first run-through
			firstTime = false;
			// Re-enable timer
			timer.Enabled = true;
		}

		List<UserEntry> FileReadUserNames()
		{
			List<UserEntry> UserEntries = new List<UserEntry>();
			// User change files are stored individually in a folder
			string folder = filepath + usersfolder;
			foreach (string filename in Directory.EnumerateFiles(folder, "*.json"))
			{
				string contents = File.ReadAllText(filename);
				try
				{
					UserEntry entry = JsonConvert.DeserializeObject<UserEntry>(contents);
					UserEntries.Add(entry);
				}
				catch (Exception e)
				{
					if (firstTime)
					{
						// Ignore bad file
						LMeventLog.WriteEntry("Cannot read file: " + filename + ", " + e.Message, EventLogEntryType.Warning, eventId++);
					}
				}
			}
			return UserEntries;
		}

		private bool ProcessSystem(string System, string SystemLoginData, List<UserEntry> UserList)
		{
			// Read systems.xml to get the server name/ip and port for this system
			Dictionary<String, Int16> ServerDetails = ReadServerDetails(System);
			// Log fail once on first scan
			if (ServerDetails.Count < 1 && firstTime)
			{
				LMeventLog.WriteEntry("No servers found for system: " + System, EventLogEntryType.Warning, eventId++);
				return false;
			}
			// Get login creds for the server
			string[] credentials = SystemLoginData.Split('/');
			if (credentials.Length == 2)
			{
				bool status = DecryptCreds(SystemLoginData, out string user, out SecureString password);
				if (!status && firstTime)
				{
					LMeventLog.WriteEntry("Cannot decrypt system credentials: " + System, EventLogEntryType.Error, eventId++);
					return false;
				}
				else
				{
					// For each server - once we are successful we stop
					bool result = false;
					foreach (var Server in ServerDetails)
					{
						result = ProcessServer(System, Server, user, password, UserList);
						if (result)
						{
							// Successfully changed users for this system
							break;
						}
					}
					if (!result && firstTime)
					{
						LMeventLog.WriteEntry("Failed actions on System: " + System, EventLogEntryType.Error, eventId++);
						return false;
					}

					return true;
				}
			}
			else
			{
				LMeventLog.WriteEntry("Cannot decrypt system credential string: " + System, EventLogEntryType.Error, eventId++);
				return false;
			}
		}

		// Return True if all user's passwords were set correctly
		private bool ProcessServer(string SystemName, KeyValuePair<string, short> server, string user, SecureString password, List<UserEntry> userList)
		{
			// Connect
			ClearScada.Client.Simple.Connection connection;
			var node = new ClearScada.Client.ServerNode(ClearScada.Client.ConnectionType.Standard, server.Key, server.Value);
			connection = new ClearScada.Client.Simple.Connection("MultiManagerService");
			try
			{
				connection.Connect(node);
			}
			catch (CommunicationsException)
			{
				LMeventLog.WriteEntry("Unable to communicate with Geo SCADA server: " + server.Key, EventLogEntryType.Error, eventId++);
				return false;
			}
			if (!connection.IsConnected)
			{
				LMeventLog.WriteEntry("Not connected with Geo SCADA server: " + server.Key, EventLogEntryType.Error, eventId++);
				return false;
			}
			// Log on as Admin
			try
			{
				connection.LogOn(user, password);
			}
			catch (AccessDeniedException)
			{
				LMeventLog.WriteEntry("Access denied to Geo SCADA server: " + server.Key, EventLogEntryType.Error, eventId++);
				return false;
			}
			catch (PasswordExpiredException)
			{
				LMeventLog.WriteEntry("Credentials expired on Geo SCADA server: " + server.Key, EventLogEntryType.Error, eventId++);
				return false;
			}
			catch (Exception e)
			{
				LMeventLog.WriteEntry("Connection Exception: " + e.Message + ", " + server.Key, EventLogEntryType.Error, eventId++);
				return false;
			}
			// Process each user - decode password
			foreach (var UserToChange in userList)
			{
				bool status = ProcessUser(connection, UserToChange, out string error);
				if (status)
				{
					// Clear server from registry for this user
					if (!ClearSystemFromUserEntry(SystemName, UserToChange))
					{
						LMeventLog.WriteEntry("Cannot clear user from list: " + SystemName + ", " + UserToChange.Name, EventLogEntryType.Error, eventId++);
						error = "Cannot clear user from list";
					}
				}
				// Output file text to log actions
				String csvline = DateTime.UtcNow.ToString("yyyy-MMM-dd HH:mm:ss") + "," + SystemName + "," + server.Key + "," + server.Value.ToString() + ","
										+ UserToChange.Name + "," + (status ? "OK" : "FAIL") + "," + error;
				try
				{
					// Format filename with 4 digit year and 2 digit month
					string filename = usagefilename + DateTime.UtcNow.ToString("-yyyy-MM") + usagefileext;
					using (System.IO.StreamWriter usagefile =
								new System.IO.StreamWriter(filepath + filename, true))
					{
						usagefile.WriteLine(csvline);
						Console.WriteLine(csvline);
					}
				}
				catch (Exception e)
				{
					LMeventLog.WriteEntry("Write log file exception: " + server.Key + ", " + e.Message, EventLogEntryType.Error, eventId++);
				}
			}
			return true;
		}

		private bool ClearSystemFromUserEntry( string SystemName, UserEntry User)
		{
			User.SystemsToChange.Remove(SystemName);

			string filename = filepath + usersfolder + "\\" + User.Name + ".json";
			if (User.SystemsToChange.Count > 0)
			{
				string output = JsonConvert.SerializeObject(User);
				try
				{
					File.WriteAllText(filename, output);
				}
				catch (Exception e)
				{
					// Ignore bad file
					LMeventLog.WriteEntry("Cannot write file: " + filename + ", " + e.Message, EventLogEntryType.Error, eventId++);
					return false;
				}
			}
			try
			{
				File.Delete(filename);
			}
			catch (Exception e)
			{
				// Ignore bad file
				LMeventLog.WriteEntry("Cannot delete file: " + filename + ", " + e.Message, EventLogEntryType.Error, eventId++);
				return false;
			}
			return true;
		}

		private bool ProcessUser(ClearScada.Client.Simple.Connection connection, UserEntry UserToChange, out string ErrorStatus)
		{
			// Find user objects
			ClearScada.Client.Simple.DBObject DBUser = null;
			ClearScada.Client.Simple.DBObjectCollection DBUsers = null;
			try
			{
				DBUsers = connection.GetObjects("CDBUser", UserToChange.Name);
			}
			catch (Exception e)
			{
				ErrorStatus = "Error finding user: " + UserToChange.Name + ", " + e.Message;
				return false;
			}
			if (DBUsers.Count != 1)
			{
				ErrorStatus = "Cannot find user: " + UserToChange.Name + ", Will not retry for this system";
				return true;
			}
			DBUser = DBUsers[0];

			// Get password
			DecryptUserCreds(UserToChange, out string UserNameCheck, out SecureString UserPass);
			if (UserNameCheck != UserToChange.Name)
			{
				ErrorStatus = "User name does not match, file tampered with: " + UserToChange.Name;
				return false;
			}
			// Set password
			// (Because you cannot use this from the API: connection.ChangeUserPassword(newuser.Id, "", "Snoopy"); )
			System.Security.SecureString OldPwd = new SecureString();
			try
			{
				connection.Server.ChangePassword(DBUser.Id, OldPwd, UserPass, ClearScada.Client.Advanced.ChangePasswordOptions.IsAdministrativeReset);
				// Success. 
				ErrorStatus = "Password Set";
			}
			catch (Exception e)
			{
				// Only this exception is not classed as an error
				if (e.Message == "Password must be different from current password.")
				{
					ErrorStatus = "Password was already this value";
				}
				else
				{
					ErrorStatus = "Error setting password. " + e.Message;
					return false;
				}
			}
			// Check InService flag to re-enable a user if account was disabled
			try
			{
				bool InService = (bool)DBUser.GetProperty("AccountEnabled");
				if (!InService && UserToChange.Enabled)
				{
					DBUser.SetProperty("AccountEnabled", true);
					ErrorStatus = "Enabled a disabled account";
				}
				else
				{
					DBUser.SetProperty("AccountEnabled", false);
					ErrorStatus = "Disabled an enabled account";
				}
			}
			catch (Exception e)
			{
				ErrorStatus = "Error enabling/disabling account. " + e.Message;
				return false;
			}
			return true;
		}

		private static Dictionary<String, Int16> ReadServerDetails( string server)
		{
			Dictionary<String, Int16> result = new Dictionary<string, short>();
			// Any errors cause abort and no servers to be found
			try
			{
				// Open Systems.XML
				XmlDocument SystemsXML = new XmlDocument();
				SystemsXML.Load("C:\\ProgramData\\Schneider Electric\\ClearSCADA\\Systems.xml");
				XmlNodeList Systems = SystemsXML.SelectNodes("Systems/System");
				foreach (XmlNode System in Systems)
				{
					var SystemName = System.Attributes.GetNamedItem("name").Value;
					if (SystemName.ToLower() == server.ToLower())
					{
						XmlNodeList Servers = System.SelectNodes("Server");
						foreach (XmlNode Server in Servers)
						{
							var ServerName = Server.Attributes.GetNamedItem("name").Value;
							short ServerPort;
							if (short.TryParse(Server.Attributes.GetNamedItem("port").Value, out ServerPort))
							{
								// Identical server names but different ports will not be supported
								// i.e. if you are using 127.0.0.1 and two different ports, then change one for "localhost"
								result.Add(ServerName, ServerPort);
							}
						}
						break;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("Error reading SYSTEMS.XML: " + e.Message);
			}
			return result;
		}

		// user name entropy combined with this constant
		private static byte[] additionalEntropy = new byte[] { 0x45, 0xF3, 0x10, 0xD3 };

		private static bool DecryptUserCreds(UserEntry entry, out string CheckName, out SecureString password)
		{
			password = new System.Security.SecureString();

			byte[] userEncBytes = StringToByteArray(entry.EncryptedName);
			byte[] passEncBytes = StringToByteArray(entry.EncryptedPassword);

			byte[] userDecBytes = ProtectedData.Unprotect(userEncBytes, additionalEntropy, DataProtectionScope.LocalMachine);
			CheckName = Encoding.UTF8.GetString(userDecBytes);

			// Decrypt password with additional entropy of the encoded user name
			byte[] passDecBytes = ProtectedData.Unprotect(passEncBytes, userEncBytes, DataProtectionScope.LocalMachine);
			string tpassword = Encoding.UTF8.GetString(passDecBytes);
			foreach (var c in tpassword)
			{
				password.AppendChar(c);
			}

			return true;
		}

		public static bool DecryptCreds(string credential, out string user, out SecureString password)
		{
			password = new System.Security.SecureString();

			string[] credentials = credential.Split('/');
			if (credentials.Length == 2)
			{
				byte[] userEncBytes = StringToByteArray(credentials[0]);
				byte[] passEncBytes = StringToByteArray(credentials[1]);

				byte[] userDecBytes = ProtectedData.Unprotect(userEncBytes, additionalEntropy, DataProtectionScope.LocalMachine);
				user = Encoding.UTF8.GetString(userDecBytes);

				// Decrypt password with additional entropy of the encoded user name
				byte[] passDecBytes = ProtectedData.Unprotect(passEncBytes, userEncBytes, DataProtectionScope.LocalMachine);
				string tpassword = Encoding.UTF8.GetString(passDecBytes);
				foreach (var c in tpassword)
				{
					password.AppendChar(c);
				}
				return true;
			}
			user = "";
			return false;
		}

		public static string ByteArrayToString(byte[] ba)
		{
			return BitConverter.ToString(ba).Replace("-", "");
		}

		public static byte[] StringToByteArray(String hex)
		{
			int NumberChars = hex.Length;
			byte[] bytes = new byte[NumberChars / 2];
			for (int i = 0; i < NumberChars; i += 2)
				bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
			return bytes;
		}

		private string MakeHash( string toh)
		{
			string salt = "EcoStruxure Geo SCADA Expert";
			System.Security.Cryptography.SHA1 sa = System.Security.Cryptography.SHA1.Create();
			byte[] pre = System.Text.Encoding.UTF32.GetBytes(toh + salt);
			byte[] h = sa.ComputeHash(pre);
			return System.Convert.ToBase64String(h, 0, 15);
		}

		protected override void OnStop()
		{
			LMeventLog.WriteEntry("GS Multi Manager Service Stopped");
		}
	}
	class UserEntry
	{
		public string Name = "";
		public string EncryptedName = "";
		public string EncryptedPassword = "";
		public List<String> SystemsToChange;
		public bool Enabled = true;
	}
}
