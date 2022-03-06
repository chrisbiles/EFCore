using App.Model.Enum;
using Core.Model.Interface;
using Core.Model.Interface.Data;
using Helper.Utility;
using Helper.Utility.Enums;

namespace App.Model.Commerce;

public class Account: IPerson, IPrimaryKeyGuid
{
    #region Fields
    private string _emailAddress;
    private string _phoneNumber;
    private DateTime? _dateOfBirth;
    #endregion

    public DateTime Created { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public Guid Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NameConcatenation => $"{FirstName} {LastName}";

    public string EmailAddress
    {
        get => _emailAddress;

        set => _emailAddress = value.EnsureValidEmail(AllowNulls.False);
    }
    public string Phone
    {
        get => _phoneNumber;

        set => _phoneNumber = value.EnsureValidUsPhone(AllowNulls.False);
    }
    public string LoginId { get; set; }
    public DateTime? DateOfBirth
    {
        get => _dateOfBirth;

        set
        {
            if (value.HasValue)
            {
                _dateOfBirth = value.Value.SetDateOfBirthValue();
            }
        }
    }
    public FavoriteColor? FavoriteColor { get; set; }
    public bool AllowEmailMarketing { get; set; }
    public bool IsActive { get; set; }

    public Guid? CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
}