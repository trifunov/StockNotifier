using Moq;
using StockNotifier.Application.Commands.CreateAlert;
using StockNotifier.Application.Repositories;
using StockNotifier.Domain.Entities;
using StockNotifier.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Unit.Tests.StockNotifier.Application.Commands
{
    public class CreateAlertHandlerTests
    {
        private readonly CreateAlertHandler _handler;
        private readonly Mock<IAlertRepository> _alertRepositoryMock;

        public CreateAlertHandlerTests() 
        {
            _alertRepositoryMock = new Mock<IAlertRepository>();
            _handler = new CreateAlertHandler(_alertRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateAlert_WhenRequestIsValid()
        {
            // Arrange
            var request = new CreateAlertRequest
            {
                Name = "Test Alert",
                StockId = 1,
                ThresholdType = ThresholdType.Over,
                ThresholdValue = 100,
                IsActive = true
            };
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);
            // Assert
            Assert.True(result);
            _alertRepositoryMock.Verify(repo => repo.CreateUpdateAlertAsync(It.IsAny<Alert>()), Times.Once);
        }
    }
}
