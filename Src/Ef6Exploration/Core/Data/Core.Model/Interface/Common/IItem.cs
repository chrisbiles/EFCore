using Core.Model.Interface.Data;

namespace Core.Model.Interface.Common;

public interface IItem : IPrimaryKeyGuid
{
    int Qty { get; set; }
    float Price { get; set; }
    float Discount { get; set; }
}