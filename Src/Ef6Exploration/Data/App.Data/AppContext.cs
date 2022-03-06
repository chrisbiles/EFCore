using App.Data.Maps.Commerce;
using App.Model.Commerce;
using Core.Model.Interface.Data;
using Helper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace App.Data
{
    public class AppContext : DbContext
    {
        private readonly string _connectionString;

        public AppContext()
        {
            _connectionString = Settings.GetSetting("AppConnectionString");
        }

        public AppContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .ConfigureWarnings(warnings =>
                    warnings.Ignore(
                        CoreEventId.DetachedLazyLoadingWarning,
                        CoreEventId.LazyLoadOnDisposedContextWarning)
                )
                .EnableSensitiveDataLogging()
                .EnableServiceProviderCaching()
                .UseSqlServer(_connectionString, options => options.EnableRetryOnFailure());
        }

        /* ----- DB SETS ----- */
        //Commerce
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* ----- MAPPINGS ----- */
            //Commerce
            modelBuilder.ApplyConfiguration(new AccountMap(modelBuilder.Entity<Account>()));
            modelBuilder.ApplyConfiguration(new CustomerMap(modelBuilder.Entity<Customer>()));


        }



        //Override of EF Core Save changes to make sure the LastModifiedDateTime property of an entity is always
        //updated to the current utc date/time value when an entity that implements ICore is updated.
        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is ICore entity) entity.LastModifiedDateTime = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is ICore entity) entity.LastModifiedDateTime = DateTime.UtcNow;
            }

            return await base.SaveChangesAsync(true, cancellationToken);
        }
    }
}
