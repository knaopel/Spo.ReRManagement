using CommandLine;

namespace Spo.ReRManagement.ConsoleApp.Options
{
    [Verb("list", true, new[] { "ls" }, HelpText = "List the Current Event Receivers on the list.")]
    public class ListReceiversOptions : BaseSharePointListOptions
    {
    }
}
