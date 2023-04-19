using CommandLine;

namespace Spo.ReRManagement.ConsoleApp.Options
{
    public class BaseEventReceiverOptions
    {
        [Option('u', "siteUrl", Required = true, HelpText = "The site url of the site to add the receiver to.")]
        public string SiteUrl { get; set; }

        [Option('l', "listName")]
        public string ListName { get; set; }

        [Option('n', "receiverName")]
        public string ReceiverName { get; set; }
    }
}
