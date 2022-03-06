using Core.Model.Interface.Data;

namespace App.Model.Commerce;

public class Customer : IPrimaryKeyGuid
{
    public DateTime Created { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public Guid Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NameConcatenation => $"{FirstName} {LastName}";

    public Guid AccountId { get; set; }
    public virtual Account Account { get; set; }
}