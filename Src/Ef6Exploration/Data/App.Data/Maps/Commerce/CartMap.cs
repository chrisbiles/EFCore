using App.Model.Commerce;
using Core.Data.Mappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Maps.Commerce;

internal class CartMap : PrimaryKeyMappingGuid<Cart>
{
    internal CartMap(EntityTypeBuilder<Cart> modelBuilder)
    {
        modelBuilder.ToTable("Cart");

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

        modelBuilder.Property(t => t.OrderId);

        //One to one relationships
        modelBuilder
            .HasOne(c => c.Order)
            .WithOne(o => o.Cart)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey<Order>(o => o.CartId);

        // One to many relationships
        modelBuilder
            .HasOne(x => x.Account)
            .WithMany(x => x.Carts)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}