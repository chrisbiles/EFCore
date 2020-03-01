using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Interfaces.Internal;

namespace Data
{
	public class CompositeKeyMapping<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, ICompositeKey
	{
		public void Configure(EntityTypeBuilder<TEntity> modelBuilder)
		{
			// Created Date
			modelBuilder.Property(t => t.Created)
				.IsRequired()
				.ValueGeneratedOnAdd()
				.HasDefaultValueSql("getutcdate()");

			// Updated Date
			modelBuilder.Property(t => t.LastModifiedDateTime)
				.IsRequired()
				.ValueGeneratedOnAddOrUpdate()
				.HasDefaultValueSql("getutcdate()");
		}
    }
}