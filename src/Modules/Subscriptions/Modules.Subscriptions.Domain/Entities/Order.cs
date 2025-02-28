﻿using CodeUp.SharedKernel.DomainObjects;

namespace Modules.Subscriptions.Domain.Entities;

public class Order : Entity, IAggregateRoot
{
    public Guid StudentId { get; private set; }
}
