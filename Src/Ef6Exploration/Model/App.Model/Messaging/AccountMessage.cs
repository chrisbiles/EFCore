using App.Model.Commerce;
using Core.Model.Interface.Communication;
using Core.Model.Interface.Data;
using Group = App.Model.Commerce.Group;

namespace App.Model.Messaging;

public class AccountMessage : IPrimaryKeyLong, IMessage
{
    public DateTime Created { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public long Id { get; set; }

    public string From { get; set; }
    public string CC { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime Sent { get; set; }

    public Guid? AccountId { get; set; }
    public virtual Account Account { get; set; }

    public Guid? GroupId { get; set; }
    public virtual Group Group { get; set; }

    public Guid? CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
}