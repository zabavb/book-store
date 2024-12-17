using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using OrderApi.Data;
using OrderApi.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;
using OrderApi.Repository;
using System.Collections.ObjectModel;
using OrderApi.Profiles;
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<OrderDbContext>(options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


/*var columnOptions = new ColumnOptions
{
    Store = { StandardColumn.Id, StandardColumn.LogEvent },
    AdditionalColumns = new Collection<SqlColumn>
    {
        new SqlColumn("Properties", System.Data.SqlDbType.NVarChar) {DataLength = -1 }
    }
};*/
try
{
    var logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.MSSqlServer(
            connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
            sinkOptions: new MSSqlServerSinkOptions
            {
                TableName = "Logs",
                AutoCreateSqlTable = true
            },
            /*        columnOptions: columnOptions,*/
            restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
        )
        .Enrich.WithProperty("LogTime", DateTime.UtcNow)
        .WriteTo.Console(outputTemplate: "{Level}: {Message} - {LogTime:yyyy-MM-dd HH:mm:ss}{NewLine}")
        .CreateLogger();

    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);
}
catch (Exception ex)
{
    throw new Exception("Logger cannot connect to the Database", ex);
}
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
/*builder.Services.AddAutoMapper(typeof(OrderProfile));*/
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OrderApi",
        Version = "v1"
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine($"{AppContext.BaseDirectory}", xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderApi");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
