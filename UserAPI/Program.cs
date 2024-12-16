using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Text;
using UserAPI.Data;
using UserAPI.Profiles;
using UserAPI.Repositories;
using UserAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var columnOptions = new ColumnOptions
{
    Store = { StandardColumn.Id, StandardColumn.LogEvent },
    AdditionalColumns = new Collection<SqlColumn>
    {
        new SqlColumn("Properties", System.Data.SqlDbType.NVarChar) { DataLength = -1 }
    }
};

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "Logs",
            AutoCreateSqlTable = true
        },
        columnOptions: columnOptions,
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
    )
    .Enrich.WithProperty("LogTime", DateTime.UtcNow)
    .WriteTo.Console(outputTemplate: "{Level}: {Message} - {LogTime:yyyy-MM-dd HH:mm:ss}{NewLine}")
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(SubscriptionProfile));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

builder.Services.AddScoped<string>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
