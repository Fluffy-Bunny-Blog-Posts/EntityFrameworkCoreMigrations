﻿using System;
using System.Threading.Tasks;
using Duende.IdentityServer.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public interface ITenantAwareSchoolContext : IDisposable
    {
        public DbSet<StudentExtra> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<ExternalService> ExternalServices { get; set; }



        public DbSet<ClientExtra> Clients { get; set; }
        public DbSet<PersistedGrantExtra> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }

        public DbSet<IdentityResource> IdentityResources { get; set; }

        public DbSet<ApiResource> ApiResources { get; set; }

        public DbSet<ApiScope> ApiScopes { get; set; }

        DbContext DbContext { get; }
        Task<int> SaveChangesAsync();

    }
}