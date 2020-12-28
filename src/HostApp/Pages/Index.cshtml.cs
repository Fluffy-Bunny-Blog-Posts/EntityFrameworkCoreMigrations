using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using HostApp.Services;
using Microsoft.EntityFrameworkCore;

namespace HostApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TenantInfo _tenantInfo;
        private readonly ITenantAwareSchoolContextAccessor _tenantAwareSchoolContextAccessor;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            TenantInfo tenantInfo,
            ITenantAwareSchoolContextAccessor tenantAwareSchoolContextAccessor,
            ILogger<IndexModel> logger)
        {
            _tenantInfo = tenantInfo;
            _tenantAwareSchoolContextAccessor = tenantAwareSchoolContextAccessor;
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            var context = _tenantAwareSchoolContextAccessor.GetTenantAwareConfigurationDbContext(_tenantInfo.Name);
            await context.DbContext.Database.MigrateAsync();
            await context.DbContext.Database.EnsureCreatedAsync();
            return RedirectToPage();
        }
    }
}
