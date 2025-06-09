using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Infrastructure.Database.Migrator
{
    public interface IDbMigrator
    {
        void Migrate();
    }
}
