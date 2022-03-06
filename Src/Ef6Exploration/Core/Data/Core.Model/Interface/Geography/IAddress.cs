using Core.Model.Enum;

namespace Core.Model.Interface.Geography;

public interface IAddress
{
    string AddressName { get; set; }
    string AddressLine1 { get; set; }
    string AddressLine2 { get; set; }
    string CompanyName { get; set; }
    string City { get; set; }
    string Province { get; set; }
    string ProvinceCode { get; set; }
    string PostalCode { get; set; }
    public string CountryCode { get; set; }
    public string Country { get; set; }
    double? Latitude { get; set; }
    double? Longitude { get; set; }
    AddressType? AddressType { get; set; }
    bool AddressVerified { get; set; }
}