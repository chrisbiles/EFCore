using Core.Model.Interface.Data;

namespace Core.Model.Interface.Geography
{
    public interface IState : IPrimaryKeyInt
    {
        string Name { get; set; }
        string Code { get; set; }
        string Type { get; set; }
    }
}