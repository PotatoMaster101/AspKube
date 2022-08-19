using Dapper;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", () => "It Works!");
app.MapGet("/shutdown", () =>
{
    var service = app.Services.GetRequiredService<IHostApplicationLifetime>();
    service.StopApplication();
});
app.MapGet("/db", async () =>
{
    await using var connection = new NpgsqlConnection(app.Configuration.GetConnectionString("Postgres"));
    return connection.Query<string>("SELECT version();");
});
app.Run();
