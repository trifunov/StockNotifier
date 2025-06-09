using StockNotifier.Domain.Entities;
using StockNotifier.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Unit.Tests.StockNotifier.Domain
{
    public class AlertTests
    {
        [Fact]
        public void Alert_Should_Initialize_With_Valid_Parameters()
        {
            // Arrange
            var alertId = 1;
            var stockSymbol = "AAPL";
            var targetPrice = 150;
            var thresholdType = ThresholdType.Over; 
            var dateTimeNow = DateTime.UtcNow;
            var stockId = 1;
            var isActive = true;
            // Act
            var alert = new Alert()
            {
                Id = alertId,
                Name = stockSymbol,
                ThresholdValue = targetPrice,
                ThresholdType = thresholdType,
                CreatedAt = dateTimeNow,
                ModifiedAt = dateTimeNow,
                StockId = stockId,
                IsActive = isActive
            };
            // Assert
            Assert.Equal(alertId, alert.Id);
            Assert.Equal(stockSymbol, alert.Name);
            Assert.Equal(targetPrice, alert.ThresholdValue);
            Assert.True(alert.IsActive);
        }
    }
}
