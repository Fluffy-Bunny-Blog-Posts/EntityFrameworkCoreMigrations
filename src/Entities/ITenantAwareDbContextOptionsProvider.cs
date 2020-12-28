using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public interface ITenantAwareDbContextOptionsProvider
    {
        void OnConfiguring(string tenantId, DbContextOptionsBuilder optionsBuilder); 
        void Configure(DbContextOptionsBuilder optionsBuilder);
    }
}