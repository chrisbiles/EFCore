using App.Model.Enum;
using App.Model.Messaging;
using Core.Model.Enum;
using Core.Model.Interface.Data;
using Core.Model.Interface.Geography;

namespace App.Model.Commerce;

public class Group : IPrimaryKeyGuid, IAddress
{
    public DateTime Created { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public Guid Id { get; set; }

    public string AddressName { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string CompanyName { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string ProvinceCode { get; set; }
    public string PostalCode { get; set; }
    public string CountryCode { get; set; }
    public string Country { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public AddressType? AddressType { get; set; }
    public bool AddressVerified { get; set; }

    public bool? IsActive { get; set; }
    public string Name { get; set; }
    public GroupType GroupType { get; set; }

    public Guid PrimaryAccountId { get; set; }

    public virtual ICollection<AccountMessage> Messages { get; set; } = new List<AccountMessage>();
    public virtual ICollection<AccountToGroup> AccountsToGroups { get; set; } = new List<AccountToGroup>();
}