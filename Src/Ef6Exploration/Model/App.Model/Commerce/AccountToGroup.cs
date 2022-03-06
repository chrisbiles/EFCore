using Core.Model.Interface.Data;

namespace App.Model.Commerce;

public class AccountToGroup : ICompositeKey
{
    public DateTime Created { get; set; }
    public DateTime LastModifiedDateTime { get; set; }

    public Guid AccountId { get; set; }
    public virtual Account Account { get; set; }

    public Guid GroupId { get; set; }
    public virtual Group Group { get; set; }
}