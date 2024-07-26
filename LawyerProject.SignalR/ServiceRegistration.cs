using LawyerProject.Application.Abstractions.Hubs;
using LawyerProject.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection collection)
        {
            collection.AddTransient<IAdvertHubService, AdvertHubService>();
            collection.AddSignalR();
        }
    }
}
