using Modules.Authentication.API.Configurations;
using Modules.Authentication.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.AddSwagger();
builder.AddSecurity();

var app = builder.Build();

app.MapOpenApi();

app.MapEndpoints();
app.UseCustomSwagger(builder);
app.UseSecurity();

app.Run();