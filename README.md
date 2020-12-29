# EntityFrameworkCoreMigrations

Order matters.

I don't know what the point of **EnsureCreatedAsync** is since it only creats the tables and not the migrations.  It appears that calling it after **MigrateAsync** is a **NO-OP**.  
Hence, just use **MigrateAsync**.

```
public async Task<IActionResult> OnPostAsync()
{
    var context = _tenantAwareSchoolContextAccessor.GetTenantAwareConfigurationDbContext(_tenantInfo.Name);
    await context.DbContext.Database.MigrateAsync();
    await context.DbContext.Database.EnsureCreatedAsync();
    return RedirectToPage();
}
```


Here I only use the Powershell command;
```
add-migration initial -c SchoolContext -o Migrations/Tenant
```
and when I added a new column to the Student Entity;
```
add-migration StudentFirstName -c SchoolContext -o Migrations/Tenant
```

I create the database on the fly as I chose to use a separate database for each tenant.
I get the SchoolContext by seeding it with a different database name and then call **MigrateAsync** on that context.

