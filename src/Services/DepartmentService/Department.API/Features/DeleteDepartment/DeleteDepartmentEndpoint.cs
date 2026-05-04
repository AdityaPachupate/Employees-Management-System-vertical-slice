using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Department.API.Features.DeleteDepartment;

public static class DeleteDepartmentEndpoint
{
    public static void MapDeleteDepartmentEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/departments/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new DeleteDepartmentCommand(id);
            var result = await sender.Send(command, cancellationToken);

            return Results.NoContent();
        })
        .WithName("DeleteDepartment")
        .WithTags("Departments")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Department")
        .WithDescription("Deletes a specific department by its unique ID.");
    }
}
