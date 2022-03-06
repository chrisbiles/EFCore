using App.Model.Commerce;
using Core.Data.Mappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Maps.Commerce;

internal class AccountMap : PrimaryKeyMappingGuid<Account>
{
    internal AccountMap(EntityTypeBuilder<Account> modelBuilder)
    {
        modelBuilder.ToTable("Account");

        //Properties
        modelBuilder.Property(t => t.FirstName)
            .HasMaxLength(64)
            .IsRequired();

        modelBuilder.Property(t => t.LastName)
            .HasMaxLength(64)
            .IsRequired();

        modelBuilder.Property(t => t.EmailAddress)
            .HasMaxLength(256)
            .IsRequired();

        modelBuilder.Property(t => t.Phone)
            .HasMaxLength(32)
            .IsRequired();

        modelBuilder.Property(t => t.LoginId)
            .HasMaxLength(16);

        modelBuilder.Property(t => t.DateOfBirth);

        modelBuilder.Property(t => t.FavoriteColor);

        modelBuilder.Property(t => t.AllowEmailMarketing)
            .HasDefaultValue(false)
            .IsRequired();

        modelBuilder.Property(t => t.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        //One to one relationships
        modelBuilder
            .HasOne(a => a.Customer)
            .WithOne(c => c.Account)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey<Customer>(c => c.AccountId);

        //Indexes
        modelBuilder
            .HasIndex(a => a.EmailAddress)
            .HasFilter("[EmailAddress] IS NOT NULL")
            .IsUnique();

        modelBuilder
            .HasIndex(a => a.Phone)
            .HasFilter("[Phone] IS NOT NULL")
            .IsUnique();

        modelBuilder
            .HasIndex(a => a.LoginId)
            .HasFilter("[LoginId] IS NOT NULL")
            .IsUnique();
    }
}

