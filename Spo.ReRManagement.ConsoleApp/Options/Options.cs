using CommandLine;

namespace Spo.ReRManagement.ConsoleApp
{
    public class Options
    {
        [Option('c', "clientId", Required = true, HelpText = "clientId for connection.")]
        public string ClientId { get; set; }

        [Option('s', "clientSecret", Required = true, HelpText = "The client secret for connection.")]
        public string ClientSecret { get; set; }

        [Option(
            'o',
            "operation",
            Required = false,
            Default = "add",
            HelpText = "The operation to perform. Options are \"add\" or \"remove\". Default is \"add.\"")]
        public string Option { get; set; }
    }
