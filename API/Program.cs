using API.Hubs;
using API.Services;
using Application;
using FluentValidation;
using Hangfire;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi("v1", options => { options.AddDocumentTransformer<OpenApiTransformer>(); });
builder.Services.AddLoggingConfiguration(builder.Configuration);
builder.Services.AddApplication()
                .AddInfrastructure(builder);
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
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Tally API";
        options.DarkMode = true;
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
        app.MapScalarApiReference(options => options
    .AddPreferredSecuritySchemes("Bearer")
    .AddHttpAuthentication("Bearer", auth =>
    {
        auth.Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...";
    }));
    });
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await db.Database.MigrateAsync(); // applies any pending migrations
}


app.UseHttpsRedirection();

app.UseGlobalExceptionHandling(app.Environment);
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.UseSerilogRequestLogging();
app.MapControllers();
app.UseHangfireDashboard("/hangfire");
app.MapHub<NotificationsHub>("hubs/notifications");
app.Run();
