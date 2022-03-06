using App.Model.Enum;
using Core.Model.Interface.Data;

namespace App.Model.Commerce;

public class InvoicePayment : IPrimaryKeyGuid
{
    public DateTime Created { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public Guid Id { get; set; }

    public string TransactionId { get; set; }
    public float TransactionAmount { get; set; }
    public TransactionType TransactionType { get; set; }
    public InvoicePaymentStatus InvoicePaymentStatus { get; set; }

    public Guid WalletId { get; set; }
    public virtual Wallet Wallet { get; set; }

    public Guid InvoiceId { get; set; }
    public virtual Invoice Invoice { get; set; }
}