using App.Model.Commerce;
using Core.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Maps.Commerce;

public class GroupMap : PrimaryKeyMappingGuid<Group>
{
    public GroupMap(EntityTypeBuilder<Group> modelBuilder)
    {
        modelBuilder.ToTable("Group");

        //Properties
        modelBuilder.Property(t => t.Name)
            .HasMaxLength(64);

        modelBuilder.Property(t => t.GroupType)
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

        modelBuilder.Property(t => t.PostalCode)
            .HasMaxLength(16);

        modelBuilder.Property(t => t.CountryCode)
            .HasMaxLength(64);

        modelBuilder.Property(t => t.Country)
            .HasMaxLength(128);

        modelBuilder.Property(t => t.Latitude);

        modelBuilder.Property(t => t.Longitude);

        modelBuilder.Property(t => t.AddressType);

        modelBuilder.Property(t => t.AddressVerified)
            .HasDefaultValue(false)
            .IsRequired();

        modelBuilder.Property(t => t.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        modelBuilder.Property(t => t.PrimaryAccountId)
            .IsRequired();
    }
}