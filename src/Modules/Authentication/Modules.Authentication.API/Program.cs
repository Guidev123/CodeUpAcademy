using CodeUp.Common.Abstractions.Mediator;
using CodeUp.Common.Notifications;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<INotificator, Notificator>();
builder.Services.AddMediator(typeof(Program));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();