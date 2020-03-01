using Data.Mappings.Commerce;
using Microsoft.EntityFrameworkCore;
using Models.Commerce;

namespace Data
{
	public class CommerceContext : DbContext
	{
		public CommerceContext()
		{
		}

		public CommerceContext(DbContextOptions options)
			: base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("");
		}

		/* ----- DB SETS ----- */
		// Commerce
		public DbSet<Customer> Customers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			/* ----- MAPPINGS ----- */
			// Commerce
			modelBuilder.ApplyConfiguration(new CustomerMap(modelBuilder.Entity<Customer>()));
		}
	}
}