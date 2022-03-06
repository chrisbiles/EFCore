using App.Model.Commerce;
using Core.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Maps.Commerce;

internal class ProductMap : PrimaryKeyMappingGuid<Product>
{
    internal ProductMap(EntityTypeBuilder<Product> modelBuilder)
    {
        modelBuilder.ToTable("Product");

        // Properties
        modelBuilder.Property(t => t.Name)
            .HasMaxLength(64)
            .IsRequired();

        modelBuilder.Property(t => t.ShortDescription)
            .HasMaxLength(1024)
            .IsRequired();

        modelBuilder.Property(t => t.Description)
            .HasMaxLength(2048)
            .IsRequired();

        modelBuilder.Property(t => t.Sku)
            .HasMaxLength(64)
            .IsRequired();

        modelBuilder.Property(t => t.Cost);

        modelBuilder.Property(t => t.Price);

        modelBuilder.Property(t => t.ProductType)
            .IsRequired();

        modelBuilder.Property(t => t.IncludeInSort)
            .HasDefaultValue(false)
            .IsRequired();

        modelBuilder.Property(t => t.ExpirationDays);

        modelBuilder.Property(t => t.StripeProductId)
            .HasMaxLength(128);

        modelBuilder.Property(t => t.StripeStatementDescriptor)
            .HasMaxLength(22);

        modelBuilder.Property(t => t.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        modelBuilder.Property(t => t.NotForSale)
            .HasDefaultValue(false)
            .IsRequired();

        //Indexes
        modelBuilder
            .HasIndex(p => p.StripeProductId)
            .HasFilter("[StripeProductId] IS NOT NULL")
            .IsUnique();

        modelBuilder
            .HasIndex(p => p.Sku)
            .IsUnique();

        modelBuilder
            .HasIndex(p => p.Name)
            .IsUnique();

        //Check Constraints
        modelBuilder
            .HasCheckConstraint("CK_ExpirationDays_Null_Or_Value", "[ExpirationDays] IS NULL OR [ExpirationDays] >= 1");
    }
}