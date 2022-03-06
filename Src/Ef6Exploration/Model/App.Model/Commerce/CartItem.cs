using Core.Model.Interface.Common;

namespace App.Model.Commerce;

public class CartItem : IItem
{
    public DateTime Created { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public Guid Id { get; set; }

    public int Qty { get; set; }
    public float Price { get; set; }
    public float Discount { get; set; }

    public Guid CartId { get; set; }
    public virtual Cart Cart { get; set; }

    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
}