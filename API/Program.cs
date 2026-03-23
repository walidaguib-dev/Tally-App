using API.Hubs;
using API.Services;
using Application;
using FluentValidation;
using Hangfire;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Jobs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System
            .Text
            .Json
            .Serialization
            .ReferenceHandler
            .IgnoreCycles;
    });
builder.Services.AddOpenApi(
    "v1",
    options =>
    {
        options.AddDocumentTransformer<OpenApiTransformer>();
    }
);
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    );
});
builder.Services.AddLoggingConfiguration(builder.Configuration);
builder.Services.AddApplication().AddInfrastructure(builder);
builder.Services.AddAPIServices();
builder.Services.AddValidations();
builder.Services.AddIdentityServices();
builder.Services.ConfigureCloudinary(builder);
builder.Services.ConfigureEmailService(builder);
builder.Services.ConfigureRedisServices(builder.Configuration);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.ConfigureBackgroundJobs(builder);
builder.Services.AddAuthenticationServices(builder);
builder.Services.AddAuthorization();
builder.Services.AddRateLimitingServices();
builder.Services.AddFusionCache(builder.Configuration);
builder.Services.AddSignalR();

builder.Host.UseSerilog();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapOpenApi();
app.MapScalarApiReference(
    "/docs",
    options =>
    {
        options.Title = "Tally API";
        options.DarkMode = true;
        options.BaseServerUrl = "https://tally-app-production.up.railway.app/";
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
        app.MapScalarApiReference(options =>
            options
                .AddPreferredSecuritySchemes("Bearer")
                .AddHttpAuthentication(
                    "Bearer",
                    auth =>
                    {
                        auth.Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...";
                    }
                )
        );
    }
);

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await db.Database.MigrateAsync(); // applies any pending migrations
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseGlobalExceptionHandling(app.Environment);
app.UseRateLimiter();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseSerilogRequestLogging();
app.MapControllers();
app.UseHangfireDashboard("/hangfire");
app.MapHub<NotificationsHub>("hubs/notifications");
RecurringJob.AddOrUpdate<QuantitySyncJob>(
    "sync-pending-quantities",
    job => job.SyncQuantities(),
    "*/2 * * * *"
);
app.Run();
