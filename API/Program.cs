using API.Services;
using Application;
using Infrastructure;
using Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddApplication()
    .AddInfrastructure();
builder.Services.AddIdentityServices();

builder.Services.AddAPIServices();
builder.Services.AddValidations();
builder.Services.AddDatabase(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
