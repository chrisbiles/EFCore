namespace Models.Interfaces.Internal
{
	public interface IPerson
	{
		string FirstName { get; set; }
		string LastName { get; set; }
		string NameConcatenation { get; }
	}
}