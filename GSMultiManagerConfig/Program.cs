using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace GSMultiManagerConfig
{
	static class Program
	{
		private static string filepath = "C:\\ProgramData\\Schneider Electric\\ClearSCADA\\GS Multi Manager\\";
		private static string usersfolder = "Users\\";

		private static RegistryKey SystemsKey = null;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// Create user folder if it does not exist
			try
			{
				Directory.CreateDirectory(filepath + usersfolder);
			}
			catch (Exception e)
			{
				MessageBox.Show("Error creating path: " + filepath + ", " + e.Message);
			}

			Application.Run(new Login() ); 
		}

		public static List<String> RegReadSystemNames()
		{
			List<String> Systems = new List<String>();
			// Check key name
			try
			{
				var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
				SystemsKey = baseKey.CreateSubKey(@"SOFTWARE\Schneider Electric\GSMultiManager\Systems",
												RegistryKeyPermissionCheck.ReadWriteSubTree);
			}
			catch (Exception e)
			{
				MessageBox.Show("Unable to open registry. " + e.Message, "GS Multi Manager Setup", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
				return Systems;
			}

			// List all key names
			foreach (string valueName in SystemsKey.GetValueNames())
			{
				Systems.Add(valueName);
			}
			return Systems;
		}
		public static string RegGetSystemCreds( string ServerName)
		{
			return (string)SystemsKey.GetValue(ServerName);
		}
		public static void RegDeleteSystem(string serverToRemove)
		{
			SystemsKey.DeleteValue(serverToRemove);
		}
		public static void RegSetSystem(string ServerName, string Value)
		{
			SystemsKey.SetValue(ServerName, Value);
		}

		public static List<UserEntry> FileReadUserNames()
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
					// Ignore bad file
					MessageBox.Show("Cannot read file: " + filename + ", " + e.Message);
				}
			}
			return UserEntries;
		}
		public static void FileSetUser(UserEntry User)
		{
			string filename = filepath + usersfolder + "\\" + User.Name + ".json";
			string output = JsonConvert.SerializeObject(User);
			try
			{
				File.WriteAllText(filename, output);
			}
			catch (Exception e)
			{
				// Ignore bad file
				MessageBox.Show("Cannot write file: " + filename + ", " + e.Message);
			}
		}
		public static void FileDeleteUser(string UserToRemove)
		{
			// Just delete the file
			string filename = filepath + usersfolder + "\\" + UserToRemove + ".json";
			try
			{
				File.Delete( filename);
			}
			catch (Exception e)
			{
				// Ignore bad file
				MessageBox.Show("Cannot delete file: " + filename + ", " + e.Message);
			}
		}

		// user name entropy combined with this constant
		private static byte[] additionalEntropy = new byte[] { 0x45, 0xF3, 0x10, 0xD3 };


		// Servers are encrypted and decrypted as user/pass
		public static bool EncryptCreds(string txtUser, string txtPassword, out string encUser, out string encPassword)
		{
			byte[] userbytes = Encoding.UTF8.GetBytes(txtUser);
			byte[] encryptedUser = System.Security.Cryptography.ProtectedData.Protect(userbytes, additionalEntropy, DataProtectionScope.LocalMachine);

			byte[] passbytes = Encoding.UTF8.GetBytes(txtPassword);
			byte[] encryptedPassword = System.Security.Cryptography.ProtectedData.Protect(passbytes, encryptedUser, DataProtectionScope.LocalMachine);

			encUser = ByteArrayToString(encryptedUser);
			encPassword = ByteArrayToString(encryptedPassword);
			return true;
		}
		public static bool DecryptCreds( string credential, out string user, out string password)
		{
			string[] credentials = credential.Split('/');
			if (credentials.Length == 2)
			{
				byte[] userEncBytes = StringToByteArray(credentials[0]);
				byte[] passEncBytes = StringToByteArray(credentials[1]);

				byte[] userDecBytes = ProtectedData.Unprotect(userEncBytes, additionalEntropy, DataProtectionScope.LocalMachine);
				user = Encoding.UTF8.GetString(userDecBytes);

				// Decrypt password with additional entropy of the encoded user name
				byte[] passDecBytes = ProtectedData.Unprotect(passEncBytes, userEncBytes, DataProtectionScope.LocalMachine);
				password = Encoding.UTF8.GetString(passDecBytes);
				return true;
			}
			user = "";
			password = "";
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
	}
	class UserEntry
	{
		public UserEntry( string _Name)
		{
			Name = _Name;
			SystemsToChange = new List<String>();
		}
		public string Name = "";
		public string EncryptedName = "";
		public string EncryptedPassword = "";
		public List<String> SystemsToChange;
		public bool Enabled = true;
	}
}
