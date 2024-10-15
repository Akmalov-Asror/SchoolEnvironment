using Environment.Api.V1.Class.Dto_s;
using Environment.Api.V1.Class.Interfaces;
using Environment.Api.V1.Class.Model;
using Environment.Api.V1.Common.Configurations;
using Environment.Api.V1.Common.ExceptionError;
using Environment.Api.V1.Common.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Environment.Api.V1.Class.Services;

public class ClassService : IClassService
{
    private readonly IRepository<Class.Entity.Class> _classRepository;

    public ClassService(IRepository<Class.Entity.Class> classRepository) => _classRepository = classRepository;

    public async ValueTask<ClassModel> AddStudentAsync(AddClassDto classDto)
    {
        if (classDto is not null)
        {
            var newClass = new Class.Entity.Class
            {
                Name = classDto.Name,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            var createdClass = await _classRepository.CreateAsync(newClass);
            await _classRepository.SaveChangesAsync();

            return new ClassModel().MapFromEntity(createdClass);
        }
        throw new SchoolException(400, "You must provide all necessary details.");
    }

    public async ValueTask<bool> DeleteStudentAsync(int classId)
    {
        var existingClass = await _classRepository.GetAsync(c => c.Id == classId)
            ?? throw new SchoolException(404, "Class not found.");

        await _classRepository.DeleteAsync(classId);
        await _classRepository.SaveChangesAsync();
        return true;
    }

    public async ValueTask<IEnumerable<ClassModel>> GetAllStudentsAsync(PaginationParams @params, Expression<Func<Class.Entity.Class, bool>> expression = null)
    {
        var classData = _classRepository.GetAll(expression: expression, isTracking: false).Include(x => x.Students);
        var classList = await classData.ToPagedList(@params).ToListAsync();
        return classList.Select(c => new ClassModel().MapFromEntity(c)).ToList();
    }

    public async ValueTask<ClassModel> GetStudentByIdAsync(int classId)
    {
        var existingClass = await _classRepository.GetAll(c => c.Id == classId).Include(x => x.Students).FirstOrDefaultAsync()
            ?? throw new SchoolException(404, "Class not found.");

        return new ClassModel().MapFromEntity(existingClass);
    }

    public async ValueTask<bool> UpdateStudentAsync(int id, AddClassDto classDto)
    {
        var existingClass = await _classRepository.GetAsync(c => c.Id == id)
            ?? throw new SchoolException(404, "Class not found.");

        // Update properties only if they have values in the DTO
        if (!string.IsNullOrWhiteSpace(classDto.Name))
        {
            existingClass.Name = classDto.Name;
        }

        existingClass.UpdatedDate = DateTime.Now;

        _classRepository.Update(existingClass);
        await _classRepository.SaveChangesAsync();
        return true;
    }
}
