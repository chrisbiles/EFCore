using App.Model.Commerce;
using Core.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Maps.Commerce;

internal class OrderItemMap : PrimaryKeyMappingGuid<OrderItem>
{
    public OrderItemMap(EntityTypeBuilder<OrderItem> modelBuilder)
    {
        modelBuilder.ToTable("OrderItem");

        //Properties
        modelBuilder.Property(t => t.Discount)
            .IsRequired();

        modelBuilder.Property(t => t.Qty)
            .IsRequired();

        modelBuilder.Property(t => t.Price)
            .IsRequired();

        // One to many relationships
        modelBuilder
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        modelBuilder
            .HasOne(x => x.Product)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}