using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace BP.DataService.WinService
{
    /// <summary>
    /// Implements Data Service Installer functionality
    /// 
    /// 2018/09/19 - Created, VTyagunov
    /// </summary>
    [RunInstaller(true)]
    public partial class DataServiceInstaller : Installer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DataServiceInstaller()
        {
            // InitializeComponent();
            serviceProcessInstaller1 = new ServiceProcessInstaller();
            serviceProcessInstaller1.Account = ServiceAccount.LocalSystem;
            serviceInstaller1 = new ServiceInstaller();
            serviceInstaller1.ServiceName = "BPDataServiceHost";
            serviceInstaller1.DisplayName = "BP: DataServiceHost";
            serviceInstaller1.Description = "BP: Data Service Host";
            serviceInstaller1.StartType = ServiceStartMode.Automatic;
            Installers.Add(serviceProcessInstaller1);
            Installers.Add(serviceInstaller1);
        }
    }
}