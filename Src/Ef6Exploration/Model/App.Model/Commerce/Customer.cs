using App.Model.Messaging;
using Core.Model.Interface.Data;

namespace App.Model.Commerce;

public class Customer : IPrimaryKeyGuid
{
    public DateTime Created { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public Guid Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NameConcatenation => $"{FirstName} {LastName}";

    public string InvoicePrefix { get; set; }
    public string StripeCustomerId { get; set; }
    public Guid? DefaultWalletId { get; set; }

    public Guid AccountId { get; set; }
    public virtual Account Account { get; set; }

    public virtual ICollection<AccountMessage> Messages { get; set; } = new List<AccountMessage>();
    public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}