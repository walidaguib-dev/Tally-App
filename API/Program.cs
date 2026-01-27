using API.Services;
using Application;
using Hangfire;
using Infrastructure;
using Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddApplication()
    .AddInfrastructure();
builder.Services.AddIdentityServices();
builder.Services.ConfigureEmailService(builder);
builder.Services.ConfigureBackgroundJobs(builder);
builder.Services.AddAPIServices();
builder.Services.AddValidations();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddAuthenticationServices(builder);    
builder.Services.AddAuthorization();
builder.Services.AddRateLimitingServices();
builder.Services.ConfigureRedisServices(builder.Configuration);
builder.Services.ConfigureCloudinary(builder);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHangfireDashboard("/hangfire");
app.Run();
