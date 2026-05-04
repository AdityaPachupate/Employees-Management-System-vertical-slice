using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Department.API.Features.GetDepartments;

public static class GetDepartmentsEndpoint
{
    public static void MapGetDepartmentsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/departments", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetDepartmentsQuery();
            var result = await sender.Send(query, cancellationToken);

            return Results.Ok(result.Departments);
        })
        .WithName("GetDepartments")
        .WithTags("Departments")
        .Produces<IEnumerable<DepartmentDto>>(StatusCodes.Status200OK)
        .WithSummary("Get All Departments")
        .WithDescription("Retrieves a list of all departments in the system.");
    }
}
