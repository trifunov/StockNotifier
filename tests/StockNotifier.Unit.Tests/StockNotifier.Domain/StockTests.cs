using StockNotifier.Domain.Entities;
using StockNotifier.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Unit.Tests.StockNotifier.Domain
{
    public class StockTests
    {
        [Fact]
        public void Stock_Should_Initialize_With_Valid_Parameters()
        {
            // Arrange
            var stockName = "Test";
            var targetPrice = 150;
            var dateTimeNow = DateTime.UtcNow;
            var stockId = 1;
            // Act
            var stock = new Stock()
            {
                Id = stockId,
                Name = stockName,
                Value = targetPrice,
                CreatedAt = dateTimeNow,
                ModifiedAt = dateTimeNow
            };
            // Assert
            Assert.Equal(stockId, stock.Id);
            Assert.Equal(stockName, stock.Name);
            Assert.Equal(targetPrice, stock.Value);
        }
    }
}
