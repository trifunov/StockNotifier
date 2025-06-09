using MediatR;
using StockNotifier.Application.Repositories;
using StockNotifier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Application.Commands.CreateAlert
{
    public class CreateAlertHandler : IRequestHandler<CreateAlertRequest, bool>
    {
        private readonly IAlertRepository _alertRepository;

        public CreateAlertHandler(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }

        public async Task<bool> Handle(CreateAlertRequest request, CancellationToken cancellationToken)
        {
            var alert = new Alert
            {
                Id = 0,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                Name = request.Name,
                StockId = request.StockId,
                ThresholdType = request.ThresholdType,
                ThresholdValue = request.ThresholdValue,
                IsActive = request.IsActive
            };

            await _alertRepository.CreateUpdateAlertAsync(alert);
            return true;
        }
    }
}
