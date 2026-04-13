using Carter;
using Microsoft.EntityFrameworkCore;
using Users.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddCarter();

var app = builder.Build();




// Configure the HTTP request pipeline
app.MapCarter();

app.Run();
