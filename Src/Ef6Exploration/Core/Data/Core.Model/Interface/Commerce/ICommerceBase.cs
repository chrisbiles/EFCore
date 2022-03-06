namespace Core.Model.Interface.Commerce;

public interface ICommerceBase
{
    float Discount { get; set; }
    float LineItemSum { get; set; }
    float Tax { get; set; }
    float SubTotal { get; set; }
    float Total { get; set; }
}