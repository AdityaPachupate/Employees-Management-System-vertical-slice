using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Department.API.Features.CreateDepartment;

public static class CreateDepartmentEndpoint
{
    public static void MapCreateDepartmentEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/departments", async (CreateDepartmentRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateDepartmentCommand(request);
            var result = await sender.Send(command, cancellationToken);

            return Results.Created($"/departments/{result.DepartmentId}", result);
        })
        .WithName("CreateDepartment")
        .WithTags("Departments")
        .Produces<CreateDepartmentResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Department")
        .WithDescription("Creates a new department in the system.");
    }
}
