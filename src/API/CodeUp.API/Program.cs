using CodeUp.API.Configurations;
using CodeUp.API.Endpoints;
using Modules.Authentication.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddSwaggerConfig();
builder.Services.AddAutheticationModule(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseSwaggerConfig();
app.MapEndpoints();

app.Run();