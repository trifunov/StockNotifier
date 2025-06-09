﻿using EvolveDb;
using Microsoft.Extensions.Configuration;
using StockNotifier.Infrastructure.Database.Connections;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Infrastructure.Database.Migrator
{
    public class DbMigrator : IDbMigrator
    {
        private readonly IDatabaseConnection _databaseConnection;

        public DbMigrator(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public void Migrate()
        {
            var paths = new string[] { "Database", "Scripts" };
            var executingAssemblyLocation = Assembly.GetExecutingAssembly().Location;
            var executingAssemblyLocationPath = Path.GetDirectoryName(executingAssemblyLocation);
            if (!string.IsNullOrWhiteSpace(executingAssemblyLocationPath))
            {
                paths = paths.Prepend(executingAssemblyLocationPath).ToArray();
            }

            using var connection = _databaseConnection.GetConnection();
            var scriptsLocation = Path.Combine(paths.ToArray());
            var evolve = new Evolve((DbConnection)connection)
            {
                IsEraseDisabled = true,
                CommandTimeout = 600,
                Locations = new[] { scriptsLocation }
            };
            evolve.Migrate();
        }
    }
}
