using CommandLine;

namespace Spo.ReRManagement.ConsoleApp.Options
{
    public class BaseEventReceiverOptions
    {
        [Option('u', "siteUrl", Required = true, HelpText = "The site url of the site to add the receiver to.")]
        public string SiteUrl { get; set; }

        [Option('l', "listName", Required = true, HelpText = "The name of the list with which to register the Event Receiver.")]
        public string ListName { get; set; }

        [Option('n', "receiverName", Required = true, HelpText = "The name of the Event Receiver.")]
        public string ReceiverName { get; set; }
    }
}
