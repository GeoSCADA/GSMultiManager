using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace GSMultiManagerSvc
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			if (Environment.UserInteractive)
			{
				GSMultiManager service1 = new GSMultiManager(); // Add args to () if needed
				service1.TestStartupAndStop(args);
			}
			else
			{
				// Put the body of your old Main method here.  
				ServiceBase[] ServicesToRun;
				ServicesToRun = new ServiceBase[]
				{
					new GSMultiManager()
				};
				ServiceBase.Run(ServicesToRun);
			}
		}
	}
}
