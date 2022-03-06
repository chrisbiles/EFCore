using Core.Model.Interface.Common;

namespace App.Model.Commerce;

public class OrderItem : IItem
{
    public DateTime Created { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public Guid Id { get; set; }

    public int Qty { get; set; }
    public float Price { get; set; }
    public float Discount { get; set; }

    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; }

    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
}