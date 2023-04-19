using CommandLine;

namespace Spo.ReRManagement.ConsoleApp.Options
{
    public class BaseEventReceiverOptions : BaseSharePointListOptions
    {
        [Option('n', "receiverName", Required = true, HelpText = "The name of the Event Receiver.")]
        public string ReceiverName { get; set; }
    }
}
