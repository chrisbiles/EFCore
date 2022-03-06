using Core.Model.Interface.Data;

namespace Core.Model.Interface.Commerce;

public interface ICommerceBase : IPrimaryKeyGuid
{
    float Discount { get; set; }
    float LineItemSum { get; set; }
    float Tax { get; set; }
    float SubTotal { get; set; }
    float Total { get; set; }
}