﻿using Core.Model.Interface.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Data.Mappings
{
	public class PrimaryKeyMappingGuid<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IPrimaryKeyGuid
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
                .HasDefaultValueSql("getutcdate()")
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            // Updated Date
            modelBuilder.Property(t => t.LastModifiedDateTime)
                .IsRequired()
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("getutcdate()")
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);
        }
    }
}