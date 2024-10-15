using Environment.Api.V1.Common.BaseEntity;

namespace Environment.Api.V1.Student.Entity;

public class Student : BaseEntity
{
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public int ClassId { get; set; }
    public Class.Entity.Class Class { get; set; }
}
