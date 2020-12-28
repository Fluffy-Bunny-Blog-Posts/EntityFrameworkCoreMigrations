namespace Entities
{
    public interface ITenantAwareSchoolContextAccessor
    {
        ITenantAwareSchoolContext GetTenantAwareConfigurationDbContext(string tenantId);
    }
}