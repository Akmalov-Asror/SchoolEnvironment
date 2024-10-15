using Environment.Api.V1.Common.Configurations;
using Environment.Api.V1.Student.Dto_s;
using Environment.Api.V1.Student.Models;
using System.Linq.Expressions;

namespace Environment.Api.V1.Student.Interfaces;

public interface IStudentsService
{
    ValueTask<StudentModel> AddStudentAsync(AddStudentDto student);
    ValueTask<StudentModel> GetStudentByIdAsync(int studentId);
    ValueTask<IEnumerable<StudentModel>> GetAllStudentsAsync(PaginationParams @params, Expression<Func<Entity.Student, bool>> expression);
    ValueTask<bool> UpdateStudentAsync(int id,AddStudentDto student);
    ValueTask<bool> DeleteStudentAsync(int studentId);
}