using Dapper;
using Microsoft.Data.SqlClient;
using Web.Api.EndPoints;
using Web.Api.Models;
using Web.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(ServiceProvider =>
{
    var configuration = ServiceProvider.GetRequiredService<IConfiguration>();

    var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ApplicationException("The connection string is null");

    return new SqlConnectionFactory(connectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCustomerEndpoints();

app.UseHttpsRedirection();

app.Run();