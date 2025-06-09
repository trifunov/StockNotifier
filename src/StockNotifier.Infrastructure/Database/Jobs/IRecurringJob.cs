using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Infrastructure.Database.Jobs
{
    public interface IRecurringJob
    {
        void RegisterRecurringJob();
    }
}
