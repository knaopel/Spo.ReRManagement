using CommandLine;

namespace Spo.ReRManagement.ConsoleApp.Options
{
    [Verb("remove",HelpText = "Remove Event Receivers with the given ReceiverName from the list.")]
    public class RemoveReceiverOptions : BaseEventReceiverOptions
    {
    }
}
