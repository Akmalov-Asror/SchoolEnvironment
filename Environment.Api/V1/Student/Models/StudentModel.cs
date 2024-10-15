namespace Environment.Api.V1.Student.Models;

public class StudentModel
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public virtual StudentModel MapFromEntity(Student.Entity.Student model)
    {
        Id = model.Id;
        CreatedDate = model.CreatedDate;
        UpdatedDate = model.UpdatedDate;
        FullName = model.FullName;
        BirthDate = model.BirthDate;
        return this;
    }
}
