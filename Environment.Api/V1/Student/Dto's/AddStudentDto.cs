using System.ComponentModel.DataAnnotations;

namespace Environment.Api.V1.Student.Dto_s;

public class AddStudentDto
{
    [Required]
    public string FullName { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    public int ClassId { get; set; }
}
