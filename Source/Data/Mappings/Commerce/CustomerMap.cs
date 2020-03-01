using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Commerce;

namespace Data.Mappings.Commerce
{
	public class CustomerMap : PrimaryKeyMapping<Customer>
	{
		public CustomerMap(EntityTypeBuilder<Customer> modelBuilder)
		{
			modelBuilder.ToTable("Customer");

			//Properties
			modelBuilder.Property(t => t.FirstName)
				.HasMaxLength(64)
				.IsRequired();

			modelBuilder.Property(t => t.LastName)
				.HasMaxLength(64)
				.IsRequired();

			modelBuilder.Property(t => t.EmailAddress)
				.HasMaxLength(256)
				.IsRequired();

			modelBuilder.Property(t => t.IsActive)
				.HasDefaultValue(true)
				.IsRequired();
		}
	}
}