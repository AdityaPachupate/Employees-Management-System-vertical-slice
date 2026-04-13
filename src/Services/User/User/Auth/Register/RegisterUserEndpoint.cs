using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Users.API.Auth.Register
{
    public record RegisterUserRequest(string Username, string Password, string Role);
    public record RegisterUserResponse(int UserId);

    public class RegisterUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/auth/register",
                async (RegisterUserRequest request, [FromServices] ISender sender) =>
                {
                    var command = request.Adapt<RegisterUserCommand>();
                    var result = await sender.Send(command);
                    var response = result.Adapt<RegisterUserResponse>();

                    return Results.Created($"/auth/users/{response.UserId}", response);
                })
                .WithName("RegisterUser")
                .Produces<RegisterUserResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Register User")
                .WithDescription("Register new user");
        }
    }
}