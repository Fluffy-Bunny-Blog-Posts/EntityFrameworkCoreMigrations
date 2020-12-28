using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace SchoolEntityFrameworkCore
{
    public class SqlServerDbContextOptionsProvider : ITenantAwareDbContextOptionsProvider
    {
        private EntityFrameworkConnectionOptions _options;
        private IServiceProvider _serviceProvider;

        public SqlServerDbContextOptionsProvider(
            IServiceProvider serviceProvider,
            IOptions<EntityFrameworkConnectionOptions> options)
        {
            _options = options.Value;
            _serviceProvider = serviceProvider;
        }
        
        public void Configure(DbContextOptionsBuilder optionsBuilder)
        {
            
            // just here to build the migrations
           var migrationsAssemblyProvider = (IMigrationsAssemblyProvider)_serviceProvider.GetService(typeof(IMigrationsAssemblyProvider));
           var connectionString = _options.ConnectionString;
           if (migrationsAssemblyProvider == null)
           {
               optionsBuilder.UseSqlServer(connectionString);
           }
           else
           {
               optionsBuilder.UseSqlServer(connectionString, o => o.MigrationsAssembly(migrationsAssemblyProvider.AssemblyName));
           }
        }
 
        public void OnConfiguring(string tenantId, DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _options.ConnectionStringDatabaseTemplate
                .Replace("{{Database}}", $"{tenantId}-database");
            var migrationsAssemblyProvider = (IMigrationsAssemblyProvider)_serviceProvider.GetService(typeof(IMigrationsAssemblyProvider));
            if (migrationsAssemblyProvider == null)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
            else
            {
                optionsBuilder.UseSqlServer(connectionString, o => o.MigrationsAssembly(migrationsAssemblyProvider.AssemblyName));
            }
        }
    }
}
