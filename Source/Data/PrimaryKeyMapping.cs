using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Interfaces.Internal;

namespace Data
{
	public class PrimaryKeyMapping<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IPrimaryKey
	{
		public void Configure(EntityTypeBuilder<TEntity> modelBuilder)
		{
			// Primary Key
			modelBuilder.HasKey(t => t.Id);
			modelBuilder.Property(t => t.Id)
				.IsRequired()
				.ValueGeneratedOnAdd()
				.HasDefaultValueSql("newid()");

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