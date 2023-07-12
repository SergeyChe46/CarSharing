using System.Text.Json.Serialization;
using CarSharing.Core.Entities.Car;
using CarSharing.Core.Services;
using CarSharing.Core.Services.Mapping;
using CarSharing.Entities.Car;
using CarSharing.Services;
using Mapster;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ./Services
builder.Services.RegisterContext(configuration);
builder.Services.ConfigureIdentity(configuration);
builder.Services.RepositoryManagerRegister();
builder.Services.ConfigureJwt();
builder.Services.CarMapper();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("swagger/v1/swagger.json", "carRent v1");
        c.RoutePrefix = "";
    });
}
app.MapControllers();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
