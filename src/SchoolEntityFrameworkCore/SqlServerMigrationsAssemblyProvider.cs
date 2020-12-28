using SchoolEntityFrameworkCore;

namespace Entities
{
    public class SqlServerMigrationsAssemblyProvider : IMigrationsAssemblyProvider
    {
        public string AssemblyName => typeof(EFMigrations.Anchor).Assembly.FullName;
    }
}