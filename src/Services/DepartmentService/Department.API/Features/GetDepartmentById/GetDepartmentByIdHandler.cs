using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Department.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Department.API.Features.GetDepartmentById;

public class GetDepartmentByIdHandler(DepartmentDbContext db, ILogger<GetDepartmentByIdHandler> logger) 
    : IQueryHandler<GetDepartmentByIdQuery, GetDepartmentByIdResponse>
{
    public async Task<GetDepartmentByIdResponse> Handle(GetDepartmentByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching department with ID: {Id}", query.Id);

        var departmentData = await db.Departments
            .Where(d => d.DepartmentId == query.Id)
            .GroupJoin(
                db.DepartmentStats,
                d => d.DepartmentId,
                s => s.DepartmentId,
                (department, stats) => new { department, stats }
            )
            .SelectMany(
                x => x.stats.DefaultIfEmpty(),
                (x, stat) => new GetDepartmentByIdResponse(
                    x.department.DepartmentId,
                    x.department.Name,
                    x.department.Description,
                    x.department.CreatedDate,
                    stat != null ? stat.EmployeeCount : 0
                )
            )
            .FirstOrDefaultAsync(cancellationToken);

        if (departmentData == null)
        {
            logger.LogWarning("Department with ID {Id} not found", query.Id);
            throw new NotFoundException("Department", query.Id);
        }

        logger.LogInformation("Successfully fetched department: {Name}", departmentData.Name);

        return departmentData;
    }
}
