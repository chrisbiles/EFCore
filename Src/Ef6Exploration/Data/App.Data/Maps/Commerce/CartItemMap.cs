using App.Model.Commerce;
using Core.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Maps.Commerce;

internal class CartItemMap : PrimaryKeyMappingGuid<CartItem>
{
    internal CartItemMap(EntityTypeBuilder<CartItem> modelBuilder)
    {
        modelBuilder.ToTable("CartItem");

        //Properties
        modelBuilder.Property(t => t.Discount)
            .IsRequired();

        modelBuilder.Property(t => t.Qty)
            .IsRequired();

        modelBuilder.Property(t => t.Price)
            .IsRequired();

        // One to many relationships
        modelBuilder
            .HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        modelBuilder
            .HasOne(ci => ci.Product)
            .WithMany(p => p.CartItems)
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}