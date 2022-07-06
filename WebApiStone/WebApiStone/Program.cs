using WebApiStone.Settings;
using WebApiStone.Services;
using WebApiStone.Business;
using FluentValidation.AspNetCore;
using WebApiStone.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IDatabaseConnection, DatabaseConnection>();
builder.Services.AddScoped<IStatisticsHub, StatisticsHub>();


builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PersonBusiness>());


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
              builder.AllowAnyHeader()
                     .AllowAnyMethod()
                     .SetIsOriginAllowed((host) => true)
                     .AllowCredentials();
            }));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.UseCors("CorsPolicy");
app.MapHub<StatisticsHub>("/statisticshub");
app.Run();
