using BuildingBlocks.CQRS;
using Department.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Department.API.Features.GetDepartments;

public class GetDepartmentsHandler(DepartmentDbContext db, ILogger<GetDepartmentsHandler> logger) 
    : IQueryHandler<GetDepartmentsQuery, GetDepartmentsResponse>
{
    public async Task<GetDepartmentsResponse> Handle(GetDepartmentsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching all departments");

        var departments = await db.Departments
            .GroupJoin(
                db.DepartmentStats,
                d => d.DepartmentId,
                s => s.DepartmentId,
                (department, stats) => new { department, stats }
            )
            .SelectMany(
                x => x.stats.DefaultIfEmpty(),
                (x, stat) => new DepartmentDto(
                    x.department.DepartmentId,
                    x.department.Name,
                    x.department.Description,
                    x.department.CreatedDate,
                    stat != null ? stat.EmployeeCount : 0
                )
            )
            .ToListAsync(cancellationToken);

        logger.LogInformation("Successfully fetched {Count} departments", departments.Count);

        return new GetDepartmentsResponse(departments);
    }
}
