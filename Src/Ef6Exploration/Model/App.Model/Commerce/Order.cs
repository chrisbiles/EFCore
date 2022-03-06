using App.Model.Enum;
using Core.Model.Interface.Commerce;

namespace App.Model.Commerce;

public class Order : ICommerceBase
{
    public DateTime Created { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public Guid Id { get; set; }

    public float Discount { get; set; }
    public float LineItemSum { get; set; }
    public float Tax { get; set; }
    public float SubTotal { get; set; }
    public float Total { get; set; }
    public int OrderNumber { get; set; }
    public DateTime OrderDate => Created;
    public OrderStatus Status { get; set; }

    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public Guid WalletId { get; set; }
    public virtual Wallet Wallet { get; set; }

    public Guid? CartId { get; set; }
    public virtual Cart Cart { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}