using App.Model.Commerce;
using Core.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Maps.Commerce;

internal class AccountAddressMap : PrimaryKeyMappingGuid<AccountAddress>
{
    internal AccountAddressMap(EntityTypeBuilder<AccountAddress> modelBuilder)
    {
        modelBuilder.ToTable("AccountAddress");

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
            .HasMaxLength(128)
            .IsRequired();

        modelBuilder.Property(t => t.AddressLine2)
            .HasMaxLength(128);

        modelBuilder.Property(t => t.CompanyName)
            .HasMaxLength(128);

        modelBuilder.Property(t => t.City)
            .HasMaxLength(64)
            .IsRequired();

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

        modelBuilder.Property(t => t.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        // One to many relationships
        modelBuilder
            .HasOne(aa => aa.Account)
            .WithMany(ac => ac.AddressBook)
            .HasForeignKey(aa => aa.AccountId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}