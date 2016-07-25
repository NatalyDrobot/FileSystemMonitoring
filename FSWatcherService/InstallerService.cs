using System.ComponentModel;
using System.ServiceProcess;

namespace FSWatcherService
{
    [RunInstaller(true)]
    public partial class InstallerService : System.Configuration.Install.Installer
    {
        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;

        public InstallerService()
        {
            InitializeComponent();
            serviceInstaller = new ServiceInstaller();
            processInstaller = new ServiceProcessInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "FSWatcherService";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
