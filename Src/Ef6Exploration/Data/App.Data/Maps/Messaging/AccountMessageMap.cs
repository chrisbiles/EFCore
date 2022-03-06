using App.Model.Messaging;
using Core.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Maps.Messaging;

internal class AccountMessageMap : PrimaryKeyMappingLong<AccountMessage>
{
    internal AccountMessageMap(EntityTypeBuilder<AccountMessage> modelBuilder)
    {
        modelBuilder.ToTable("AccountMessage");

        //Properties
        modelBuilder.Property(t => t.From)
            .HasMaxLength(256)
            .IsRequired();

        modelBuilder.Property(t => t.CC)
            .HasMaxLength(256);

        modelBuilder.Property(t => t.Title)
            .HasMaxLength(512)
            .IsRequired();

        modelBuilder.Property(t => t.Body)
            .HasMaxLength(2048)
            .IsRequired();

        modelBuilder.Property(t => t.Sent)
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("getutcdate()");

        // One to many relationships
        modelBuilder
            .HasOne(am => am.Account)
            .WithMany(ac => ac.Messages)
            .HasForeignKey(am => am.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .HasOne(am => am.Group)
            .WithMany(hh => hh.Messages)
            .HasForeignKey(am => am.GroupId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .HasOne(am => am.Customer)
            .WithMany(hh => hh.Messages)
            .HasForeignKey(am => am.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}