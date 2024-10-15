using Environment.Api.V1.Common.Configurations;
using Environment.Api.V1.Common.ExceptionError;
using Environment.Api.V1.Common.Repository;
using Environment.Api.V1.Student.Dto_s;
using Environment.Api.V1.Student.Interfaces;
using Environment.Api.V1.Student.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Environment.Api.V1.Student.Services;

public class StudentsService : IStudentsService
{
    private readonly IRepository<Student.Entity.Student> _studentsRepository;
    private readonly IRepository<Class.Entity.Class> _classRepository;

    public StudentsService(IRepository<Entity.Student> studentsRepository, IRepository<Class.Entity.Class> classRepository)
    {
        _studentsRepository = studentsRepository;
        _classRepository = classRepository;
    }

    public async ValueTask<StudentModel> AddStudentAsync(AddStudentDto student)
    {
        var data = new Student.Entity.Student();


        var findClass = await _classRepository.GetAsync(x => x.Id == student.ClassId);
        if (findClass != null)
        {
            data.ClassId = student.ClassId;

        }else
        {

        throw new SchoolException(404, "Class is not found");
        }

        if (student is not null)
        {
            data.CreatedDate = DateTime.Now;
            data.BirthDate = student.BirthDate;
            data.FullName = student.FullName;
            data.UpdatedDate = DateTime.Now;
            var createdstudent = await _studentsRepository.CreateAsync(data);
            await _studentsRepository.SaveChangesAsync();

            return new StudentModel().MapFromEntity(createdstudent);
        }
        throw new SchoolException(400, "You have to complete all");
    }

    public async ValueTask<bool> DeleteStudentAsync(int studentId)
    {
        var findstudent = await _studentsRepository.GetAsync(p => p.Id == studentId)
            ?? throw new SchoolException(404, "Student is not found");

        await _studentsRepository.DeleteAsync(studentId);
        await _studentsRepository.SaveChangesAsync();
        return true;
    }

    public async ValueTask<IEnumerable<StudentModel>> GetAllStudentsAsync(PaginationParams @params, Expression<Func<Entity.Student, bool>> expression = null)
    {
        var data = _studentsRepository.GetAll(expression: expression, isTracking: false);
        var studentsList = await data.ToPagedList(@params).ToListAsync();
        return studentsList.Select(e => new StudentModel().MapFromEntity(e)).ToList();
    }

    public async ValueTask<StudentModel> GetStudentByIdAsync(int studentId)
    {
        var student = await _studentsRepository.GetAsync(x => x.Id == studentId);
        if (student is null)
            throw new SchoolException(404, "user_not_found");

        return new StudentModel().MapFromEntity(student);
    }

    public async ValueTask<bool> UpdateStudentAsync(int id, AddStudentDto student)
    {
        var studentById = await _studentsRepository.GetAsync(x => x.Id == id);

        if (studentById is null)
            throw new SchoolException(404, "Student is not found");

        // Update properties only if they have values in the DTO
        if (!string.IsNullOrWhiteSpace(student.FullName))
        {
            studentById.FullName = student.FullName;
        }

        if (student.BirthDate != null)
        {
            studentById.BirthDate = student.BirthDate;
        }
        if (student.ClassId == 0)
        {
            throw new SchoolException(404, "Class is not found");

        }
        if (student.ClassId != null && student.ClassId > 0)
        {
            var findClass = await _classRepository.GetAsync(x => x.Id == student.ClassId);
            if (findClass == null)
            {
                throw new SchoolException(404, "Class is not found");
            }
            studentById.ClassId = student.ClassId;
        }

        studentById.UpdatedDate = DateTime.Now;

        _studentsRepository.Update(studentById);
        return true;
    }
}
