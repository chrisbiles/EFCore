using App.Model.Commerce;
using Core.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Maps.Commerce;

internal class InvoicePaymentMap : PrimaryKeyMappingGuid<InvoicePayment>
{
    internal InvoicePaymentMap(EntityTypeBuilder<InvoicePayment> modelBuilder)
    {
        modelBuilder.ToTable("InvoicePayment");

        //Properties
        modelBuilder.Property(t => t.TransactionId)
            .HasMaxLength(64)
            .IsRequired();

        modelBuilder.Property(t => t.TransactionAmount)
            .IsRequired();

        modelBuilder.Property(t => t.TransactionType)
            .IsRequired();

        modelBuilder.Property(t => t.InvoicePaymentStatus)
            .IsRequired();

        //One to many relationships
        modelBuilder
            .HasOne(ip => ip.Invoice)
            .WithMany(i => i.InvoicePayments)
            .HasForeignKey(ip => ip.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        modelBuilder
            .HasOne(ip => ip.Wallet)
            .WithMany(w => w.InvoicePayments)
            .HasForeignKey(ip => ip.WalletId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}