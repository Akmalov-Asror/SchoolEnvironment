using Environment.Api.V1.Class.Interfaces;
using Environment.Api.V1.Class.Services;
using Environment.Api.V1.Common.Data;
using Environment.Api.V1.Common.ExceptionError;
using Environment.Api.V1.Common.Middlewares;
using Environment.Api.V1.Common.Repository;
using Environment.Api.V1.Student.Interfaces;
using Environment.Api.V1.Student.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SQLiteConnection");
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.Converters.Add(new StringEnumConverter());

});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IClassService,ClassService>();
builder.Services.AddScoped<IStudentsService, StudentsService>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(connectionString);
    options.EnableDetailedErrors();
});

builder.Services.AddSwaggerGen(p =>
{
    p.EnableAnnotations();
    p.SwaggerDoc("v1", new OpenApiInfo { Title = "Students API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Students API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<SchoolExceptionMiddleware>();

app.MapControllers();

app.Run();
