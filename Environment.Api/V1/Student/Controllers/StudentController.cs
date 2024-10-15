using Environment.Api.V1.Common.Configurations;
using Environment.Api.V1.Common.ResponseHandlers;
using Environment.Api.V1.Student.Dto_s;
using Environment.Api.V1.Student.Entity;
using Environment.Api.V1.Student.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace Environment.Api.V1.Student.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentsService _studentsService;

    public StudentController(IStudentsService studentsService) => _studentsService = studentsService;

    [HttpPost]
    [SwaggerOperation(Summary = "Add Student", Description = "You can Add new Student here ")]
    public async Task<IActionResult> AddStudentAsync([FromForm]AddStudentDto studentDto) 
        => ResponseHandler.ReturnIActionResponse(await _studentsService.AddStudentAsync(studentDto));

    [HttpGet]
    [SwaggerOperation(Summary = "Get Student By Id", Description = "You can get Student by Id")]
    public async Task<IActionResult> GetStudentByIdAsync(int studentId) 
        => ResponseHandler.ReturnIActionResponse(await _studentsService.GetStudentByIdAsync(studentId));

    [HttpGet("all")]
    [SwaggerOperation(Summary = "Get Students", Description = "You can get all Students")]
    public async Task<IActionResult> GetAllStudentsAsync([FromQuery] PaginationParams paginationParams) 
        => ResponseHandler.ReturnIActionResponse(await _studentsService.GetAllStudentsAsync(paginationParams, null));


    [HttpPut]
    [SwaggerOperation(Summary = "Update Student information", Description = "You can update everything from Student")]
    public async Task<IActionResult> UpdateStudentAsync(int studentId, [FromBody] AddStudentDto studentDto)
            => ResponseHandler.ReturnIActionResponse(await _studentsService.UpdateStudentAsync(studentId, studentDto));


    [HttpDelete]
    [SwaggerOperation(Summary = "Delete Student", Description = "You can delete Student")]
    public async Task<IActionResult> DeleteStudentAsync(int studentId)
         => ResponseHandler.ReturnIActionResponse(await _studentsService.DeleteStudentAsync(studentId));

}
