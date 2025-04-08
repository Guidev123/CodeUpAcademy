using Modules.Authentication.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.AddSwagger();
builder.AddSecurity();

var app = builder.Build();

app.MapOpenApi();

app.UseCustomSwagger();
app.UseSecurity();

app.Run();