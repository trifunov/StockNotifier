using Microsoft.AspNetCore.SignalR;
using StockNotifier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Infrastructure.SignalRServer
{
    public class AlertHub : Hub, IAlertHub
    {
        public async Task SendEqualAlert(Alert alert)
        {
            await Clients.All.SendAsync("ReceiveEqualAlert", alert);
        }

        public async Task SendOverAlert(Alert alert)
        {
            await Clients.All.SendAsync("ReceiveOverAlert", alert);
        }

        public async Task SendUnderAlert(Alert alert)
        {
            await Clients.All.SendAsync("ReceiveUnderAlert", alert);
        }
    }
}
