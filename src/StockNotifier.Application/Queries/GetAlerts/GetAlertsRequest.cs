using MediatR;
using StockNotifier.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Application.Queries.GetAlerts
{
    public sealed record GetAlertsRequest : IRequest<GetAlertsResponse>
    {
        public bool OnlyActive { get; set; }
    }
}
