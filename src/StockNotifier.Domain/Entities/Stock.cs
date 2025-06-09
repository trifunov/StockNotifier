using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Domain.Entities
{
    public class Stock : BaseEntity
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
