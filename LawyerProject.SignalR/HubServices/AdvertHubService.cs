using LawyerProject.Application.Abstractions.Hubs;
using LawyerProject.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.SignalR.HubServices
{
    public class AdvertHubService : IAdvertHubService
    {
        readonly IHubContext<AdvertHub> _hubContext;

        public AdvertHubService(IHubContext<AdvertHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task AdvertAddedMessageAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.AdvertAddedMessage, message);
        }
    }
}
