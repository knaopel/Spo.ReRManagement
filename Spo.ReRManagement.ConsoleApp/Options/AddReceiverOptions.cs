using CommandLine;
using System.Collections.Generic;

namespace Spo.ReRManagement.ConsoleApp.Options
{
    [Verb("add", HelpText = "Add Event Receiver to list.")]
    public class AddReceiverOptions : BaseEventReceiverOptions
    {
        [Option('t', "receiverTypes", Required = true, Separator = ',', HelpText = "A comma separated list of types to add.")]
        public IEnumerable<string> ReceiverTypes { get; set; }

        [Option('r', "receiverUrl", Required = true, HelpText = "The Url of the Event Receiver.")]
        public string ReceiverUrl { get; set; }
    }
}
