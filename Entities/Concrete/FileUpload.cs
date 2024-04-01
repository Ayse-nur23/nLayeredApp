using Core.Entities;

namespace Entities.Concrete;

public class FileUpload : Entity<Guid>
{
    public  string FileName { get; set; }
    public string FilePath { get; set; }

}