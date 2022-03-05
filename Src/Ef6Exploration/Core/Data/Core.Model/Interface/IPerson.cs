namespace Core.Model.Interface
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string NameConcatenation { get; }
    }
}