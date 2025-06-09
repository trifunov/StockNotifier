using StockNotifier.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Domain.Entities
{
    public class Alert : BaseEntity
    {
        public string Name { get; set; }

        public ThresholdType ThresholdType { get; set; }

        public int ThresholdValue { get; set; }

        public int StockId { get; set; }

        public bool IsActive { get; set; }
    }
}
