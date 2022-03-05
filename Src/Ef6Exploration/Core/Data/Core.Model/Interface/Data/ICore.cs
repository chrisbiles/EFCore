namespace Core.Model.Interface.Data
{
    public interface ICore
    {
        DateTime Created { get; set; } 
        DateTime LastModifiedDateTime { get; set; }
    }
}