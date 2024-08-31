//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//  app.UseSwagger();
//  app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();



using RollsApi.Data;
using RollsApi.Extensions;

try
{
  var builder = WebApplication.CreateBuilder(args);

  Log.Logger = new LoggerConfiguration().WriteTo.PostgreSQL(
                  connectionString: builder.Configuration.GetConnectionString("EFConn"),
                  tableName: "rolls_logs",
                  columnOptions: null,
                  restrictedToMinimumLevel: LogEventLevel.Information,
                  needAutoCreateTable: true).Enrich.WithMachineName().
                  Enrich.FromLogContext().
                  Enrich.WithWebApiControllerName().Enrich.WithWebApiActionName().Enrich.WithHttpRequestType().CreateLogger();

  builder.Services.AddControllers();
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen(c =>
  {
    c.OperationFilter<SwaggerCustomHeader>();
  });
  builder.Services.CustomServices();

  builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(

          builder.Configuration.GetConnectionString("EFConn")

          ));

  builder.Services.AddSingleton<DapperContext>();

  builder.Host.UseSerilog();

  var app = builder.Build();

  if (app.Environment.IsDevelopment())
  {
    app.UseSwagger();
    app.UseSwaggerUI();
  }

  AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

  app.UseAuthorization();

  app.MapControllers();

  app.Run();
}
catch (Exception ex)
{
  Log.Fatal(ex, "Admin API startup failed due to {reason}", ex.GetBaseException().Message);
}
finally
{
  Log.CloseAndFlush();
}