using Environment.Api.V1.Common.BaseEntity;

namespace Environment.Api.V1.Class.Entity;

public class Class : BaseEntity
{
    public string Name { get; set; }
    public List<Student.Entity.Student>? Students { get; set; }
}
