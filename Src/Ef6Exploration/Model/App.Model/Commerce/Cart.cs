using Core.Model.Interface.Commerce;

namespace App.Model.Commerce;

public class Cart : ICommerceBase
{
    public DateTime Created { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public Guid Id { get; set; }

    public float Discount { get; set; }
    public float LineItemSum { get; set; }
    public float Tax { get; set; }
    public float SubTotal { get; set; }
    public float Total { get; set; }

    public Guid? AccountId { get; set; }
    public virtual Account Account { get; set; }

    //For Conversion Metrics
    public Guid? OrderId { get; set; }
    public virtual Order Order { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}