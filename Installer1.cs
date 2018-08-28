using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Linq;
using System.Threading.Tasks;

namespace WindowsService1
{
    [RunInstaller(true)]
    public partial class Installer1 : Installer
    {
        ServiceProcessInstaller processInstaler;
        ServiceInstaller serviceInstaller;

        public Installer1()
        {
            processInstaler = new ServiceProcessInstaller();
            processInstaler.Account = ServiceAccount.LocalSystem;

            serviceInstaller = new ServiceInstaller();
            serviceInstaller.ServiceName = "[====TEST SERVICE====]";
            serviceInstaller.Description = "My First NT (Windiws) service!!!";
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            base.Installers.Add(processInstaler);
            base.Installers.Add(serviceInstaller);
        }
    }
}
