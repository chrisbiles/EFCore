using Core.Model.Enum;
using Core.Model.Interface;
using Core.Model.Interface.Data;
using Helper.Utility;

namespace App.Model.Commerce;

public class Wallet : IPrimaryKeyGuid, IPerson
{
    #region Private Properties
    private DateTime _cardExpiration;
    #endregion

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
    public AddressType AddressType { get; set; }
    public bool AddressVerified { get; set; }

    public string CardNickName { get; set; }
    public string CardLast4 { get; set; }
    public DateTime CardExpiration
    {
        get => _cardExpiration;

        set => _cardExpiration = value.CardExpirationValue();
    }
    public string StripePaymentMethodId { get; set; }
    public bool IsActive { get; set; }

    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
}