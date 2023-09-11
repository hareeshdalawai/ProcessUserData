using Microsoft.EntityFrameworkCore;
using UserDb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FunctionAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));
var app = builder.Build();



app.Run();

