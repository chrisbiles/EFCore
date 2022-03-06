using File = Core.Model.Common.File;

namespace App.Model.Commerce;

public class ProductImage : File
{
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
}