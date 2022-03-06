using App.Model.Commerce;
using Core.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Maps.Commerce;

internal class OrderMap : PrimaryKeyMappingGuid<Order>
{
    internal OrderMap(EntityTypeBuilder<Order> modelBuilder)
    {
        modelBuilder.ToTable("Order");

        //Properties
        modelBuilder.Property(t => t.Discount)
            .IsRequired();

        modelBuilder.Property(t => t.LineItemSum)
            .IsRequired();

        modelBuilder.Property(t => t.Tax)
            .IsRequired();

        modelBuilder.Property(t => t.SubTotal)
            .IsRequired();

        modelBuilder.Property(t => t.Total)
            .IsRequired();

        modelBuilder.Property(t => t.OrderNumber)
            .UseIdentityColumn(1000)
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        modelBuilder.Property(t => t.Status)
            .IsRequired();

        // One to many relationships
        modelBuilder
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        modelBuilder
            .HasOne(o => o.Wallet)
            .WithMany(w => w.Orders)
            .HasForeignKey(o => o.WalletId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}