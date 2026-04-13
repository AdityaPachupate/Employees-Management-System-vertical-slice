using BuildingBlocks.CQRS;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Users.API.Data;
using Users.API.Models;

namespace Users.API.Auth.Register
{
    public record RegisterUserCommand(string Username, string Password, string Role)
        : ICommand<RegisterUserResult>;

    public record RegisterUserResult(int UserId);

    internal class RegisterUserCommandHandler(UserDbContext dbContext)
        : ICommandHandler<RegisterUserCommand, RegisterUserResult>
    {
        public async Task<RegisterUserResult> Handle(
            RegisterUserCommand command,
            CancellationToken cancellationToken)
        {
            var exists = await dbContext.Users
                .AnyAsync(u => u.Username == command.Username, cancellationToken);

            if (exists)
                throw new Exception("Username already exists");

            var user = new User
            {
                Username = command.Username,
                Role = command.Role,
                CreatedAt = DateTime.UtcNow
            };

            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, command.Password);

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new RegisterUserResult(user.UserId);
        }
    }
}