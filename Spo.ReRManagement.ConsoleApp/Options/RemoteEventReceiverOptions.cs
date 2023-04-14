using System.Collections.Generic;
using System;

namespace Spo.ReRManagement.ConsoleApp.Options
{
    public class RemoteEventReceiverOptions
    {
        public string Name { get; set; } = null!;

        public string ListName { get; set; } = null!;

        public Uri ReceiverUrl { get; set; }

        public string ReceiverTypes { get; set; }

        public IEnumerable<string> ReceiverTypeArray => this.ReceiverTypes.Split(',');
    }
}
