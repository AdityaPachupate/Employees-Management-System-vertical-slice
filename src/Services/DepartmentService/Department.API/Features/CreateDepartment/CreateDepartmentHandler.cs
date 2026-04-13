using BuildingBlocks.CQRS;
using Department.API.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Department.API.Features.CreateDepartment;

public class CreateDepartmentHandler(DepartmentDbContext db, ILogger<CreateDepartmentHandler> logger) 
    : ICommandHandler<CreateDepartmentCommand, CreateDepartmentResponse>
{
    public async Task<CreateDepartmentResponse> Handle(CreateDepartmentCommand command, CancellationToken cancellationToken)
    {
        // Uniqueness check
        var alreadyExists = await db.Departments.AnyAsync(x => x.Name == command.Request.Name, cancellationToken);
        if (alreadyExists)
        {
            logger.LogWarning("Department with name {Name} already exists.", command.Request.Name);
            throw new BuildingBlocks.Exceptions.BadRequestException($"Department with name '{command.Request.Name}' already exists.");
        }

        var department = command.Request.Adapt<Domain.Department>();
        
        db.Departments.Add(department);
        await db.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Department {DepartmentId} created successfully.", department.DepartmentId);

        return department.Adapt<CreateDepartmentResponse>();
    }
}
