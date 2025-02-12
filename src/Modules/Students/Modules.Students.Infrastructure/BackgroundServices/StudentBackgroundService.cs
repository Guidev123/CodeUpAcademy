﻿using CodeUp.Common.Responses;
using CodeUp.IntegrationEvents;
using CodeUp.IntegrationEvents.Authentication;
using CodeUp.MessageBus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.Students.Application.Commands.Create;

namespace Modules.Students.Infrastructure.BackgroundServices;

public class StudentBackgroundService(IServiceProvider serviceProvider, IMessageBus bus) : BackgroundService
{
    private readonly IMessageBus _bus = bus;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetResponse();
        return Task.CompletedTask;
    }

    private void SetResponse()
    {
        _bus.RespondAsync<RegisteredUserIntegrationEvent, IntegrationEventResponseMessage>(RegisterStudent);
        _bus.AdvancedBus.Connected += OnConnect!;
    }

    private void OnConnect(object s, EventArgs e) => SetResponse();

    private async Task<IntegrationEventResponseMessage> RegisterStudent(RegisteredUserIntegrationEvent message)
    {
        Response<CreateStudentResponse> response;

        using (var scope = _serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var command = new CreateStudentCommand(message.Id, message.FirstName,
                                                   message.LastName, message.Email, message.Phone,
                                                   message.Document, message.BirthDate,
                                                   message.ProfilePicture, message.Type);

            response = await mediator.Send(command);
        }

        return response.IsSuccess ? new IntegrationEventResponseMessage(true) : new IntegrationEventResponseMessage(false, [.. response.Errors!]);
    }
}
