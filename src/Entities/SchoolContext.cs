using System;
using System.Threading.Tasks;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Extensions;
using Duende.IdentityServer.EntityFramework.Options;
using Entities.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class SchoolContext : DbContext, ITenantAwareSchoolContext
    {
        private string _tenantId;
        private ITenantAwareDbContextOptionsProvider _dbContextOptionsProvider;
        private ConfigurationStoreOptions _storeOptions;
        private OperationalStoreOptions _operationalStoreOptions;

        public SchoolContext(ConfigurationStoreOptions storeOptions, OperationalStoreOptions operationalStoreOptions,DbContextOptions<SchoolContext> options)
            : base(options)
        {
            this._storeOptions = storeOptions ?? throw new ArgumentNullException(nameof(storeOptions));
            this._operationalStoreOptions = operationalStoreOptions ?? throw new ArgumentNullException(nameof(operationalStoreOptions));
        }
        public SchoolContext(
            string tenantId,
            ConfigurationStoreOptions storeOptions,
            OperationalStoreOptions operationalStoreOptions,
            ITenantAwareDbContextOptionsProvider dbContextOptionsProvider)
        {

            this._storeOptions = storeOptions ?? throw new ArgumentNullException(nameof(storeOptions));
            this._operationalStoreOptions = operationalStoreOptions ?? throw new ArgumentNullException(nameof(operationalStoreOptions));
            _tenantId = tenantId ?? throw new ArgumentNullException(nameof(tenantId));
            _dbContextOptionsProvider = dbContextOptionsProvider ?? throw new ArgumentNullException(nameof(dbContextOptionsProvider));
           
        }
        public DbContext DbContext => this;
        public DbSet<StudentExtra> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ClientExtra> Clients { get; set; }
        public DbSet<ExternalService> ExternalServices { get; set; }
        public DbSet<PersistedGrantExtra> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
        public DbSet<IdentityResource> IdentityResources { get; set; }
        public DbSet<ApiResource> ApiResources { get; set; }
        public DbSet<ApiScope> ApiScopes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrWhiteSpace(_tenantId))
            {
                base.OnConfiguring(optionsBuilder);
            }
            else
            {
                _dbContextOptionsProvider.OnConfiguring(_tenantId, optionsBuilder);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ConfigureStudentContext();
            modelBuilder.ConfigureExternalServicesContext();
            modelBuilder.ConfigureClientContext(_storeOptions);
            modelBuilder.ConfigureResourcesContext(_storeOptions);
            modelBuilder.ConfigurePersistedGrantContext(_operationalStoreOptions);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }

}
