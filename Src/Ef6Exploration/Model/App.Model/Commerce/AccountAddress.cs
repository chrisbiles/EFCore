using Core.Model.Enum;
using Core.Model.Interface;
using Core.Model.Interface.Data;
using Core.Model.Interface.Geography;

namespace App.Model.Commerce;

public class AccountAddress : IPrimaryKeyGuid, IPerson, IAddress
{
    public DateTime Created { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public Guid Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NameConcatenation => $"{FirstName} {LastName}";

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

    public Guid AccountId { get; set; }
    public virtual Account Account { get; set; }
}