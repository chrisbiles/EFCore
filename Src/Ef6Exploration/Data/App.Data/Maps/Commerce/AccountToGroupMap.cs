using App.Model.Commerce;
using Core.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Maps.Commerce;

public class AccountToGroupMap : CompositeKeyMapping<AccountToGroup>
{
    public AccountToGroupMap(EntityTypeBuilder<AccountToGroup> modelBuilder)
    {
        modelBuilder.ToTable("AccountToGroup");

        //Set Composite Key
        modelBuilder
            .HasKey(x => new { x.AccountId, x.GroupId });

        //Many to Many Relationships
        modelBuilder
            .HasOne(x => x.Account)
            .WithMany(x => x.AccountsToGroups)
            .HasForeignKey(x => x.AccountId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .HasOne(x => x.Group)
            .WithMany(x => x.AccountsToGroups)
            .HasForeignKey(x => x.GroupId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}