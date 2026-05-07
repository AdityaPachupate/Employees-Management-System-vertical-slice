using BuildingBlocks.Logging;
using BuildingBlocks.Behaviors;
using Department.API.Data;
using Department.API.Features.CreateDepartment;
using Department.API.Features.GetDepartments;
using Department.API.Features.GetDepartmentById;
using Department.API.Features.UpdateDepartment;
using Department.API.Features.DeleteDepartment;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<DepartmentDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddCentralLogging("Department.API", builder.Configuration["LoggingApiUrl"] ?? "https://localhost:7182");

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Map Endpoints
app.MapCreateDepartmentEndpoint();
app.MapGetDepartmentsEndpoint();
app.MapGetDepartmentByIdEndpoint();
app.MapUpdateDepartmentEndpoint();
app.MapDeleteDepartmentEndpoint();

app.Run();
