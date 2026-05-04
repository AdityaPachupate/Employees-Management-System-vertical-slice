using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Department.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Department.API.Features.UpdateDepartment;

public class UpdateDepartmentHandler(DepartmentDbContext db, ILogger<UpdateDepartmentHandler> logger) 
    : ICommandHandler<UpdateDepartmentCommand, UpdateDepartmentResponse>
{
    public async Task<UpdateDepartmentResponse> Handle(UpdateDepartmentCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Attempting to update department ID: {Id}", command.Request.DepartmentId);

        var department = await db.Departments.FindAsync([command.Request.DepartmentId], cancellationToken: cancellationToken);

        if (department == null)
        {
            logger.LogWarning("Update failed: Department {Id} not found", command.Request.DepartmentId);
            throw new NotFoundException("Department", command.Request.DepartmentId);
        }

        var nameConflicts = await db.Departments
            .AnyAsync(d => d.Name == command.Request.Name && d.DepartmentId != command.Request.DepartmentId, cancellationToken);
        
        if (nameConflicts)
        {
            logger.LogWarning("Update failed: Department with name {Name} already exists.", command.Request.Name);
            throw new BadRequestException($"Department with name '{command.Request.Name}' already exists.");
        }

        department.Name = command.Request.Name;
        department.Description = command.Request.Description;

        db.Departments.Update(department);
        await db.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Department {Id} updated successfully", department.DepartmentId);

        return new UpdateDepartmentResponse(true);
    }
}
