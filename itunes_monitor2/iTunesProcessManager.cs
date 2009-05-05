using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ServiceProcess;
using System.ComponentModel;


namespace itunes_monitor
{
    class iTunesProcessManager
    {
    	public static void startiTunesServices()
        {
		    foreach (ServiceController service in ServiceController.GetServices()) {
			    if (Exists(_serviceNames,service.ServiceName)) {
					    if (service.Status == ServiceControllerStatus.Stopped)
						    service.Start();
			    } 
		    }
        }

        public static void killiTunesServices()
        {
            BackgroundWorker killiTunesWorker = new BackgroundWorker();
            killiTunesWorker.DoWork += new DoWorkEventHandler(killiTunesServicesBackgroundTask_DoWork);
            killiTunesWorker.RunWorkerAsync();
        }

		private static void killiTunesServicesBackgroundTask_DoWork(object sender, DoWorkEventArgs e)
        {
            //clean up real services
            foreach (ServiceController service in ServiceController.GetServices())
            {
                if (Exists(_serviceNames, service.ServiceName))
                {
                    if (service.Status == ServiceControllerStatus.Running)
                        service.Stop();
                }
            }

            System.Threading.Thread.Sleep(1000); //allow termination time

            //clean up remaining processes nicely
            foreach (Process process in System.Diagnostics.Process.GetProcesses())
            {
                if (Exists(_servicelist, process.ProcessName))
                {
                    process.CloseMainWindow();
                }
            }
            

            System.Threading.Thread.Sleep(2000); //allow termination time

            //If any processes remain after given a chance terminate them in the most burtal fashion possible
            foreach (Process process in System.Diagnostics.Process.GetProcesses())
            {
                if (Exists(_servicelist, process.ProcessName))
                {
                    process.Kill();
                }
            }
        }

		public static bool startiTunes()
        {
	        string iTunesPath = (string)Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\iTunes.exe","","ERROR");
		    bool retval = false;


		    if (iTunesPath == null || iTunesPath == "ERROR")
		    {
			    System.Windows.Forms.MessageBox.Show("Cannot find itunes are you sure it's installed?\nProgram will now show animation without launching iTunes");
			    killiTunesServices();
			    retval = true;
		    }
		    else
		    {
			    while(!isiTunesServicesRunning()) {
				    System.Threading.Thread.Sleep(100);
			    }
			    System.Diagnostics.Process.Start(iTunesPath);
		    }

		    return retval;
        }

		public static bool isiTunesServicesRunning()
        {
		    bool found = false;
		    foreach(string proc in _servicelist) 
            {
			    found = found || Process.GetProcessesByName(proc).Length > 0 ;
		    }
		    return found;
        }

		public static bool isiTunesRunning()
        {
            return Process.GetProcessesByName(_itunes).Length > 0;
        }



    	private const string _itunes = "iTunes";
		private static readonly string[] _servicelist = {"iPodService", "AppleMobileDeviceService", "SyncServer", "distnoted", "iTunesHelper"};
		private static readonly string[] _serviceNames ={"Apple Mobile Device", "iPod Service"};

		private static bool Exists(string[] list, string matchstr)
        {
            bool found = false;
	        foreach (string item in list) 
            {
		        if (item.Equals(matchstr)) 
                {
			        found = true;
                    break;
                }
            }
	        return found;

        }
    }
}
