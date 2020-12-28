using System;
using SchoolEntityFrameworkCore;

namespace Entities
{
    public class TenantAwareSchoolContextAccessor : ITenantAwareSchoolContextAccessor
    {

        private IServiceProvider _serviceProvider;
        private ITenantAwareDbContextOptionsProvider _dbContextOptionsProvider;

        public TenantAwareSchoolContextAccessor(
            IServiceProvider serviceProvider,
            ITenantAwareDbContextOptionsProvider dbContextOptionsProvider)
        {
            _serviceProvider = serviceProvider;
            _dbContextOptionsProvider = dbContextOptionsProvider;
        }
        public ITenantAwareSchoolContext GetTenantAwareConfigurationDbContext(string tenantId)
        {
            var dbContext = new SchoolContext(tenantId, _dbContextOptionsProvider);
            return dbContext;
        }
    }
}