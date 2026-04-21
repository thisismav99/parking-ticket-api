using Application;
using Infrastructure;
using ParkingTicketingAPI.Utilities.Extensions;
using ParkingTicketingAPI.Utilities.Helpers;
using Persistence;

// Configure environment variables
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") 
{ 
    ENVFiles.Exists(".env.development");
}
else
{
    ENVFiles.Exists(".env.production");
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterPersistence(builder.Configuration);
builder.Services.RegisterInfrastructure();
builder.Services.RegisterApplication();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RateLimiting();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
