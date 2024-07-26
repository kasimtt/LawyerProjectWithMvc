using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Abstractions.Hubs
{
    public interface IAdvertHubService
    {
        Task AdvertAddedMessageAsync(string message);
    }
}
