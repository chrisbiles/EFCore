using Core.Model.Interface.Data;

namespace App.Model.Commerce;

public class Invoice : IPrimaryKeyGuid
{
    public DateTime Created { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public Guid Id { get; set; }

    public int InvoiceNumber { get; set; }
    public float Cost { get; set; }
    public float Price { get; set; }
    public bool IsPaid { get; set; }

    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; }

    public virtual ICollection<InvoicePayment> InvoicePayments { get; set; } = new List<InvoicePayment>();
}