using Environment.Api.V1.Student.Models;

namespace Environment.Api.V1.Class.Model;

public class ClassModel
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string Name { get; set; }
    public List<Student.Models.StudentModel>? Students { get; set; }

    public virtual ClassModel MapFromEntity(Class.Entity.Class model)
    {
        Id = model.Id;
        CreatedDate = model.CreatedDate;
        UpdatedDate = model.UpdatedDate;
        Name = model.Name;
        Students = model.Students != null ? model.Students.Select(students => new Student.Models.StudentModel().MapFromEntity(students)).ToList() : null;
        return this;
    }

}
