using App.Data.Maps.Commerce;
using App.Data.Maps.Messaging;
using App.Model.Commerce;
using App.Model.Messaging;
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
        public DbSet<AccountAddress> AccountAddresses { get; set; }
        public DbSet<AccountMessage> AccountMessages { get; set; }
        public DbSet<AccountToGroup> AccountToGroups { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* ----- MAPPINGS ----- */
            //Commerce
            modelBuilder.ApplyConfiguration(new AccountMap(modelBuilder.Entity<Account>()));
            modelBuilder.ApplyConfiguration(new AccountAddressMap(modelBuilder.Entity<AccountAddress>()));
            modelBuilder.ApplyConfiguration(new AccountMessageMap(modelBuilder.Entity<AccountMessage>()));
            modelBuilder.ApplyConfiguration(new AccountToGroupMap(modelBuilder.Entity<AccountToGroup>()));
            modelBuilder.ApplyConfiguration(new CartMap(modelBuilder.Entity<Cart>()));
            modelBuilder.ApplyConfiguration(new CartItemMap(modelBuilder.Entity<CartItem>()));
            modelBuilder.ApplyConfiguration(new CustomerMap(modelBuilder.Entity<Customer>()));
            modelBuilder.ApplyConfiguration(new GroupMap(modelBuilder.Entity<Group>()));
            modelBuilder.ApplyConfiguration(new OrderMap(modelBuilder.Entity<Order>()));
            modelBuilder.ApplyConfiguration(new OrderItemMap(modelBuilder.Entity<OrderItem>()));
            modelBuilder.ApplyConfiguration(new ProductMap(modelBuilder.Entity<Product>()));
            modelBuilder.ApplyConfiguration(new ProductImageMap(modelBuilder.Entity<ProductImage>()));
            modelBuilder.ApplyConfiguration(new WalletMap(modelBuilder.Entity<Wallet>()));
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
