using MediatR;
using StockNotifier.Application.Commands.CreateAlert;
using StockNotifier.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Application.Queries.GetAlerts
{
    public class GetAlertsHandler : IRequestHandler<GetAlertsRequest, GetAlertsResponse>
    {
        private readonly IAlertRepository _alertRepository;

        public GetAlertsHandler(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }

        public async Task<GetAlertsResponse> Handle(GetAlertsRequest request, CancellationToken cancellationToken)
        {
            var alerts = await _alertRepository.GetAlertsAsync();
            return new GetAlertsResponse
            {
                Data = alerts
            };
        }
    }
}
