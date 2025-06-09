using Moq;
using StockNotifier.Application.Queries.GetAlerts;
using StockNotifier.Application.Repositories;
using StockNotifier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Unit.Tests.StockNotifier.Application.Queries
{
    public class GetAlertsHandlerTests
    {
        private readonly GetAlertsHandler _handler;
        private readonly Mock<IAlertRepository> _alertRepositoryMock;   

        public GetAlertsHandlerTests() 
        {
            _alertRepositoryMock = new Mock<IAlertRepository>();
            _handler = new GetAlertsHandler(_alertRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfAlerts_WhenAlertsExist()
        {
            // Arrange
            var expectedAlerts = new List<Alert>
            {
                new Alert { Id = 1, Name = "Alert 1", IsActive = true },
                new Alert { Id = 2, Name = "Alert 2", IsActive = false }
            };
            _alertRepositoryMock.Setup(repo => repo.GetAlertsAsync())
                .ReturnsAsync(expectedAlerts);
            // Act
            var result = await _handler.Handle(new GetAlertsRequest(), CancellationToken.None);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedAlerts.Count, result.Data.Count);
            Assert.Equal(expectedAlerts[0].Id, result.Data[0].Id);
            Assert.Equal(expectedAlerts[1].Id, result.Data[1].Id);
        }
    }
}
