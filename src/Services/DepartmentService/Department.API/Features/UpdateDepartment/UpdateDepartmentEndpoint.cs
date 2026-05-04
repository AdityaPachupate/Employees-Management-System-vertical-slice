using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Department.API.Features.UpdateDepartment;

public static class UpdateDepartmentEndpoint
{
    public static void MapUpdateDepartmentEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/departments", async (UpdateDepartmentRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new UpdateDepartmentCommand(request);
            var result = await sender.Send(command, cancellationToken);

            return Results.NoContent();
        })
        .WithName("UpdateDepartment")
        .WithTags("Departments")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Department")
        .WithDescription("Updates an existing department's details.");
    }
}
