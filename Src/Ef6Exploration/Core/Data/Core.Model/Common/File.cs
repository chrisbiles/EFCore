using Core.Model.Interface.Data;

namespace Core.Model.Common;

public class File : IPrimaryKeyGuid
{
    public DateTime Created { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public Guid Id { get; set; }

    public string FileName { get; set; }
    public string FileUrl { get; set; }

    public void Upload()
    {
        throw new NotImplementedException();
    }
}