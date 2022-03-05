namespace Core.Model.Interface.Data
{
    public interface IPrimaryKeyGuid : ICore
    {
        Guid Id { get; set; }
    }
}