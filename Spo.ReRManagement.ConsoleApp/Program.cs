﻿using Microsoft.Extensions.Configuration;
using Microsoft.SharePoint.Client;
using PnP.Framework;
using Spo.ReRManagement.ConsoleApp.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;

namespace Spo.ReRManagement.ConsoleApp
{
    static class Program
    {
        private static async Task Main(string[] args)
        {
            Type[] types = { typeof(AddReceiverOptions) };
            var result = Parser.Default.ParseArguments(args, types);
            await result.WithParsedAsync(RunAsync);

            //if (operation == "remove") { await RemoveEventReceiverAsync(list, remoteEventReceiver.Name); }
        }

        private static async Task RunAsync(object obj)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false).AddJsonFile("appsettings.local.json", true).Build();
            var options = configuration.Get<ConfigurationOptions>();
            var sharePointCredentials = options.SharePointAppCredentials;
            var authManager = new AuthenticationManager();
            var baseOptions = (BaseEventReceiverOptions)obj;

            using (var context = authManager.GetACSAppOnlyContext(
                       baseOptions.SiteUrl,
                       sharePointCredentials.ClientId,
                       sharePointCredentials.ClientSecret))
            {
                context.Load(context.Web);
                var list = context.Web.GetListByName(baseOptions.ListName);
                await context.ExecuteQueryRetryAsync();

                switch (obj)
                {
                    case AddReceiverOptions a:
                        foreach (var receiverTypeStr in a.ReceiverTypes)
                        {
                            if (Enum.TryParse<EventReceiverType>(receiverTypeStr, out var receiverType))
                            {
                                await AddEventReceiverAsync(list, a.ReceiverName, $"{a.ReceiverUrl}", receiverType);
                            }
                        }

                        break;
                }
            }
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
