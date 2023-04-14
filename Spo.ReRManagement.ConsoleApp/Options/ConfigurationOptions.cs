using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spo.ReRManagement.ConsoleApp.Options
{
    public class ConfigurationOptions
    {
        public SharePointAppCredentialOptions SharePointAppCredentials { get; set; } = null!;

        public RemoteEventReceiverOptions RemoteEventReceiver { get; set; } = null!;
    }
}
