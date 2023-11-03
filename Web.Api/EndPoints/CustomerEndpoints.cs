using Dapper;
using Microsoft.Data.SqlClient;
using Web.Api.Models;
using Web.Api.Services;

namespace Web.Api.EndPoints
{
    public static class CustomerEndpoints
    {
        public static void MapCustomerEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapGet("customers", async (SqlConnectionFactory sqlConnectionFactory) =>
            {
                using var connection = sqlConnectionFactory.Create();

                const string sql = "SELECT Id, FirstName, LastName, Email, DateOfBirth FROM Customers";

                var customers = await connection.QueryAsync<Customer>(sql);

                return Results.Ok(customers);
            });

            builder.MapGet("cutomers/{id}", async (int id, SqlConnectionFactory sqlConnectionFactory) =>
            {
                using var connection = sqlConnectionFactory.Create();

                const string sql = "SELECT Id, FirstName, LastName, Email, DateOfBirth FROM Customers WHERE Id = @CustomerId";

                var customer = await connection.QuerySingleOrDefaultAsync<Customer>(
                    sql,
                    new { CustomerId = id });

                return customer is not null ? Results.Ok(customer) : Results.NotFound();
            });
        }
    }
}
