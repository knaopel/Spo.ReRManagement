using System;
using CommandLine;
using Microsoft.Extensions.Configuration;

namespace Spo.ReRManagement.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(o => { Console.WriteLine($"ClientId: {o.ClientId}."); });
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
        }
    }
}
