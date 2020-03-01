using Models.Interfaces.Internal;

namespace Models.Interfaces.Commerce
{
	public interface ICustomer : IPrimaryKey, IPerson
	{
		string EmailAddress { get; set; }
		bool IsActive { get; set; }
	}
}