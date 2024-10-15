using Environment.Api.V1.Class.Dto_s;
using Environment.Api.V1.Class.Model;
using Environment.Api.V1.Common.Configurations;
using System.Linq.Expressions;

namespace Environment.Api.V1.Class.Interfaces;

public interface IClassService
{
    ValueTask<ClassModel> AddStudentAsync(AddClassDto @class);
    ValueTask<ClassModel> GetStudentByIdAsync(int classId);
    ValueTask<IEnumerable<ClassModel>> GetAllStudentsAsync(PaginationParams @params, Expression<Func<Entity.Class, bool>> expression);
    ValueTask<bool> UpdateStudentAsync(int id, AddClassDto student);
    ValueTask<bool> DeleteStudentAsync(int classId);
}
