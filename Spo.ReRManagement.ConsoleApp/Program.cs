using Microsoft.Extensions.Configuration;
using Microsoft.SharePoint.Client;
using PnP.Framework;
using Spo.ReRManagement.ConsoleApp.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spo.ReRManagement.ConsoleApp
{
    static class Program
    {
        private static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false).AddJsonFile("appsettings.local.json", true).Build();

            var options = configuration.Get<ConfigurationOptions>();
            var sharePointCredentials = options.SharePointAppCredentials;
            var remoteEventReceiver = options.RemoteEventReceiver;
            var operation = args[0];

            var authManager = new AuthenticationManager();

            var context = authManager.GetACSAppOnlyContext(
                sharePointCredentials.SiteUrl,
                sharePointCredentials.ClientId,
                sharePointCredentials.ClientSecret);

            context.Load(context.Web);
            var list = context.Web.GetListByName(remoteEventReceiver.ListName);
            await context.ExecuteQueryRetryAsync();

            if (operation == "add")
            {
                foreach (var receiverTypeStr in remoteEventReceiver.ReceiverTypeArray)
                {
                    if (Enum.TryParse<EventReceiverType>(receiverTypeStr, out var receiverType))
                    {
                        await AddEventReceiverAsync(list, remoteEventReceiver.Name, $"{remoteEventReceiver.ReceiverUrl}", receiverType);
                    }
                }
            }

            if (operation == "remove") { await RemoveEventReceiverAsync(list, remoteEventReceiver.Name); }
        }

        private static async Task AddEventReceiverAsync(List list, string receiverName, string receiverUrl, EventReceiverType type)
        {
            var eventReceiver = new EventReceiverDefinitionCreationInformation
            {
                EventType = type, ReceiverName = receiverName, ReceiverUrl = receiverUrl, SequenceNumber = 1000,
            };

            list.EventReceivers.Add(eventReceiver);

            await list.Context.ExecuteQueryRetryAsync();
        }

        private static async Task RemoveEventReceiverAsync(List list, string receiverName)
        {
            var receivers = list.EventReceivers;
            list.Context.Load(receivers);

            await list.Context.ExecuteQueryRetryAsync();

            var toDelete = receivers.ToList().FindAll(r => r.ReceiverName == receiverName);

            var receiverIds = new List<Guid>();

            // get the ids to remove into a list
            foreach (var receiver in toDelete) { receiverIds.Add(receiver.ReceiverId); }

            // iterate through Id list and remove
            foreach (var receiverId in receiverIds)
            {
                var receiverToDelete = receivers.ToList().First(r => r.ReceiverId == receiverId);
                receiverToDelete.DeleteObject();
                await list.Context.ExecuteQueryRetryAsync();
            }
        }
    }
}
