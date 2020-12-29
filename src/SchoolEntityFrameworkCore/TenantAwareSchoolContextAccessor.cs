using System;
using Duende.IdentityServer.EntityFramework.Options;
using SchoolEntityFrameworkCore;

namespace Entities
{
    public class TenantAwareSchoolContextAccessor : ITenantAwareSchoolContextAccessor
    {

        private IServiceProvider _serviceProvider;
        private ITenantAwareDbContextOptionsProvider _dbContextOptionsProvider;
        private ConfigurationStoreOptions _storeOptions;
        private OperationalStoreOptions _operationalStoreOptions;

        public TenantAwareSchoolContextAccessor(
            IServiceProvider serviceProvider,
            ConfigurationStoreOptions storeOptions,
            OperationalStoreOptions operationalStoreOptions,
            ITenantAwareDbContextOptionsProvider dbContextOptionsProvider)
        {
            _serviceProvider = serviceProvider;
            _storeOptions = storeOptions;
            _operationalStoreOptions = operationalStoreOptions;
            _dbContextOptionsProvider = dbContextOptionsProvider;
        }
        public ITenantAwareSchoolContext GetTenantAwareConfigurationDbContext(string tenantId)
        {
            var dbContext = new SchoolContext(tenantId, _storeOptions, _operationalStoreOptions,_dbContextOptionsProvider);
            return dbContext;
        }
    }
}