using Core.Model.Interface.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Data.Mappings
{
    public class CompositeKeyMapping<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, ICompositeKey
    {
        public void Configure(EntityTypeBuilder<TEntity> modelBuilder)
        {
            // Created Date
            modelBuilder.Property(t => t.Created)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("getutcdate()")
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            // Updated Date
            modelBuilder.Property(t => t.LastModifiedDateTime)
                .IsRequired()
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("getutcdate()");
        }
    }
}