using CodeUp.API.Configurations;
using CodeUp.API.Endpoints;
using Modules.Authentication.Infrastructure;
using Modules.Students.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.AddSecurity();
builder.AddCorsConfig();
builder.AddCommonConfig();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutheticationModule(builder.Configuration);
builder.Services.AddStudentModule(builder.Configuration);

var app = builder.Build();
app.UseSecurity();
app.MapEndpoints();
app.Run();