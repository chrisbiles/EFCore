using App.Model.Commerce;
using Core.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Maps.Commerce;

internal class WalletMap : PrimaryKeyMappingGuid<Wallet>
{
	public WalletMap(EntityTypeBuilder<Wallet> modelBuilder)
	{
		modelBuilder.ToTable("Wallet");

		//Properties
		modelBuilder.Property(t => t.FirstName)
			.HasMaxLength(64)
			.IsRequired();

		modelBuilder.Property(t => t.LastName)
			.HasMaxLength(64)
			.IsRequired();

		modelBuilder.Property(t => t.AddressName)
			.HasMaxLength(128);

		modelBuilder.Property(t => t.AddressLine1)
			.HasMaxLength(128);

		modelBuilder.Property(t => t.AddressLine2)
			.HasMaxLength(128);

		modelBuilder.Property(t => t.CompanyName)
			.HasMaxLength(128);

		modelBuilder.Property(t => t.City)
			.HasMaxLength(64);

		modelBuilder.Property(t => t.Province)
			.HasMaxLength(64);

		modelBuilder.Property(t => t.ProvinceCode)
			.HasMaxLength(64);

		modelBuilder.Property(t => t.CountryCode)
			.HasMaxLength(64);

		modelBuilder.Property(t => t.Country)
			.HasMaxLength(128);

		modelBuilder.Property(t => t.PostalCode)
			.HasMaxLength(16);

		modelBuilder.Property(t => t.Latitude);

		modelBuilder.Property(t => t.Longitude);

		modelBuilder.Property(t => t.AddressType)
			.IsRequired();

		modelBuilder.Property(t => t.AddressVerified)
			.HasDefaultValue(false)
			.IsRequired();

		modelBuilder.Property(t => t.CardNickName)
			.HasMaxLength(32);

		modelBuilder.Property(t => t.CardLast4)
			.HasMaxLength(4)
			.IsRequired();

		modelBuilder.Property(t => t.CardExpiration)
			.IsRequired();

		modelBuilder.Property(t => t.StripePaymentMethodId)
			.HasMaxLength(128)
			.IsRequired();

		modelBuilder.Property(t => t.IsActive)
			.HasDefaultValue(true)
			.IsRequired();

		// One to many relationships
		modelBuilder
			.HasOne(w => w.Customer)
			.WithMany(c => c.Wallets)
			.HasForeignKey(w => w.CustomerId)
			.OnDelete(DeleteBehavior.Restrict)
			.IsRequired();

		//Indexes
		modelBuilder
			.HasIndex(w => w.StripePaymentMethodId)
			.IsUnique();
	}
}