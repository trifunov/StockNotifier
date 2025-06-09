using StockNotifier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Infrastructure.SignalRServer
{
    public interface IAlertHub
    {
        Task SendOverAlert(Alert alert);
        Task SendUnderAlert(Alert alert);
        Task SendEqualAlert(Alert alert);
    }
}
