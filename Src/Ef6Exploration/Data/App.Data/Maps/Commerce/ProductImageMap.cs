using App.Model.Commerce;
using Core.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Maps.Commerce;

internal class ProductImageMap : PrimaryKeyMappingGuid<ProductImage>
{
    internal ProductImageMap(EntityTypeBuilder<ProductImage> modelBuilder)
    {
        modelBuilder.ToTable("ProductImage");

        //Properties
        modelBuilder.Property(t => t.FileName)
            .HasMaxLength(512)
            .IsRequired();

        modelBuilder.Property(t => t.FileUrl)
            .HasMaxLength(2048)
            .IsRequired();

        // One to many relationships
        modelBuilder
            .HasOne(x => x.Product)
            .WithMany(x => x.ProductImages)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        //Indexes
        modelBuilder
            .HasIndex(f => f.FileName)
            .IsUnique();

        modelBuilder
            .HasIndex(f => f.FileUrl)
            .IsUnique();
    }
}