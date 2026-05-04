using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Department.API.Features.GetDepartmentById;

public static class GetDepartmentByIdEndpoint
{
    public static void MapGetDepartmentByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/departments/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetDepartmentByIdQuery(id);
            var result = await sender.Send(query, cancellationToken);

            return Results.Ok(result);
        })
        .WithName("GetDepartmentById")
        .WithTags("Departments")
        .Produces<GetDepartmentByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Department By Id")
        .WithDescription("Retrieves a specific department by its unique ID.");
    }
}
