using App.Model.Commerce;
using Core.Data.Mappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Maps.Commerce;

internal class InvoiceMap : PrimaryKeyMappingGuid<Invoice>
{
    internal InvoiceMap(EntityTypeBuilder<Invoice> modelBuilder)
    {
        modelBuilder.ToTable("Invoice");

        // Properties
        modelBuilder.Property(t => t.InvoiceNumber)
            .UseIdentityColumn(1000, 13)
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        modelBuilder.Property(t => t.Cost)
            .IsRequired();

        modelBuilder.Property(t => t.Price)
            .IsRequired();

        modelBuilder.Property(t => t.IsPaid)
            .HasDefaultValue(false)
            .IsRequired();

        // One to many relationships
        modelBuilder
            .HasOne(i => i.Customer)
            .WithMany(c => c.Invoices)
            .HasForeignKey(i => i.CustomerId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        modelBuilder
            .HasOne(i => i.Order)
            .WithMany(o => o.Invoices)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}