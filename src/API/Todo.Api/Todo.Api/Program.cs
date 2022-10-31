using Todo.Api.Application;
using Todo.Api.Identity;
using Todo.Api.Infrastructure;
using Todo.Api.Middleware;
using Todo.Api.Persistance;
using FluentValidation;
using Hangfire;
using Hangfire.SqlServer;
using Todo.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddEndpointsApiExplorer();

IConfiguration configuration = builder.Configuration;

builder.Services
    .AddIdentityServices(configuration)
    .AddPersistenceServices(configuration)
    .AddInfrastructureServices(configuration)
    .AddApplicationServices();

#region ==== HANGFIRE configuration ====
var connectionString = configuration.GetConnectionString("HangfireConnectionString");

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    }));

builder.Services.AddHangfireServer();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();

app.UseCors("Open");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

RecurringJob.AddOrUpdate("SendNotificationsToUsers",
    () => new TaskListNotificationService().SendNotifications(),
        Cron.Minutely());

app.Run();

