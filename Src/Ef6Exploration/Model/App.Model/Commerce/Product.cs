using App.Model.Enum;
using Core.Model.Interface.Data;
using Helper.Utility;

namespace App.Model.Commerce;

public class Product : IPrimaryKeyGuid
{
    #region Fields
    private string _stripeStatementDescriptor;
    #endregion

    public DateTime Created { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public string Sku { get; set; }
    public float? Cost { get; set; }
    public float? Price { get; set; }
    public ProductType ProductType { get; set; }
    public bool IncludeInSort { get; set; }
    public int? ExpirationDays { get; set; }
    public string StripeProductId { get; set; }
    public string StripeStatementDescriptor
    {
        get => _stripeStatementDescriptor;

        set => _stripeStatementDescriptor = value.ValidateMaxSize(22);
    }
    public bool IsActive { get; set; }
    public bool NotForSale { get; set; }

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}