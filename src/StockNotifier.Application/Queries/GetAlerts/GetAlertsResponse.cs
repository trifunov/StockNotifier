using StockNotifier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Application.Queries.GetAlerts
{
    public class GetAlertsResponse
    {
        public List<Alert> Data { get; set; }
    }
}
