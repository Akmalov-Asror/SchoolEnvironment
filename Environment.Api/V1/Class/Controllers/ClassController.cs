using Environment.Api.V1.Class.Dto_s;
using Environment.Api.V1.Class.Entity;
using Environment.Api.V1.Class.Interfaces;
using Environment.Api.V1.Common.Configurations;
using Environment.Api.V1.Common.ResponseHandlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace Environment.Api.V1.Class.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassController : ControllerBase
{
    private readonly IClassService _classService;

    public ClassController(IClassService classService) => _classService = classService;

    [HttpPost]
    [SwaggerOperation(Summary = "Add Class", Description = "You can Add new Class here ")]

    public async ValueTask<IActionResult> AddClassAsync([FromForm] AddClassDto classDto)
         => ResponseHandler.ReturnIActionResponse(await _classService.AddStudentAsync(classDto));

    

    [HttpGet("id")]
    [SwaggerOperation(Summary = "Get Class By Id", Description = "You can get Class by Id")]

    public async ValueTask<IActionResult> GetClassByIdAsync(int classId)
        => ResponseHandler.ReturnIActionResponse(await _classService.GetStudentByIdAsync(classId));


    [HttpGet]
    [SwaggerOperation(Summary = "Get Classes", Description = "You can get all Classes")]

    public async ValueTask<IActionResult> GetAllClassesAsync([FromQuery] PaginationParams paginationParams)
            => ResponseHandler.ReturnIActionResponse(await _classService.GetAllStudentsAsync(paginationParams, null));


    [HttpPut]
    [SwaggerOperation(Summary = "Update Class information", Description = "You can update everything from Class")]

    public async ValueTask<IActionResult> UpdateClassAsync(int classId, [FromBody] AddClassDto classDto)
        => ResponseHandler.ReturnIActionResponse(await _classService.UpdateStudentAsync(classId, classDto));


    [HttpDelete]
    [SwaggerOperation(Summary = "Delete Class", Description = "You can delete Class")]

    public async ValueTask<IActionResult> DeleteClassAsync(int classId)
        => ResponseHandler.ReturnIActionResponse(await _classService.DeleteStudentAsync(classId));

}
