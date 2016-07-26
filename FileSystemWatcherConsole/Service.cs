using System;
using System.ServiceProcess;
using System.Threading;

namespace FileSystemWatcherConsole
{
    static class Service
    {
        public static void StartService(string[] path, string nameService)
        {
            ServiceController sc = new ServiceController(nameService);
            if (sc.Status == ServiceControllerStatus.Stopped)
            {
                sc.Start(path);
                while (sc.Status == ServiceControllerStatus.Stopped)
                {
                    Thread.Sleep(1000);
                    sc.Refresh();
                }
            }
            else
            {
                sc.Stop();
                while (sc.Status == ServiceControllerStatus.Running)
                {
                    Thread.Sleep(2000);
                    sc.Refresh();
                }
               // Console.WriteLine(sc.Status);

                sc.Start(path);
                while (sc.Status == ServiceControllerStatus.Stopped)
                {
                    Thread.Sleep(2000);
                    sc.Refresh();
                }
            }
        }
    }
}
