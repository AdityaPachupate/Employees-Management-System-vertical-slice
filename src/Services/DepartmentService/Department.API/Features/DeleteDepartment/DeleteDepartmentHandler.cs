using BuildingBlocks.Logging;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Department.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Department.API.Features.DeleteDepartment;

public class DeleteDepartmentHandler(DepartmentDbContext db, ILogger<DeleteDepartmentHandler> logger, ILogSender logSender) 
    : ICommandHandler<DeleteDepartmentCommand, DeleteDepartmentResponse>
{
    public async Task<DeleteDepartmentResponse> Handle(DeleteDepartmentCommand command, CancellationToken cancellationToken)
    {
        // This sends a log to the Centralized Logging Service
        await logSender.SendLogAsync($"Attempting to delete department ID: {command.Id}");

        logger.LogInformation("Attempting to delete department ID: {Id}", command.Id);

        var department = await db.Departments.FindAsync([command.Id], cancellationToken: cancellationToken);

        if (department == null)
        {
            logger.LogWarning("Delete failed: Department {Id} not found", command.Id);
            throw new NotFoundException("Department", command.Id);
        }

        // Remove associated stats to avoid orphaned records
        var stats = await db.DepartmentStats.FindAsync([command.Id], cancellationToken: cancellationToken);
        if (stats != null)
        {
            db.DepartmentStats.Remove(stats);
        }

        db.Departments.Remove(department);
        await db.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Department {Id} deleted successfully", command.Id);

        return new DeleteDepartmentResponse(true);
    }
}
