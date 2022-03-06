using App.Model.Commerce;
using Core.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Maps.Commerce;

internal class CustomerMap : PrimaryKeyMappingGuid<Customer>
{
    internal CustomerMap(EntityTypeBuilder<Customer> modelBuilder)
    {
        modelBuilder.ToTable("Customer");

        //Properties
        modelBuilder.Property(t => t.FirstName)
            .HasMaxLength(64)
            .IsRequired();

        modelBuilder.Property(t => t.LastName)
            .HasMaxLength(64)
            .IsRequired();

        modelBuilder.Property(t => t.InvoicePrefix)
            .HasMaxLength(11)
            .IsRequired();

        modelBuilder.Property(t => t.StripeCustomerId)
            .HasMaxLength(128)
            .IsRequired();

        modelBuilder.Property(t => t.DefaultWalletId);

        //Indexes
        modelBuilder
            .HasIndex(c => c.StripeCustomerId)
            .IsUnique();

        modelBuilder
            .HasIndex(c => c.InvoicePrefix)
            .IsUnique();
    }
}