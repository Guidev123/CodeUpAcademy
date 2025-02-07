﻿namespace Modules.Authentication.Domain.Entities;

public class Role
{
    public Role(string name, long id)
    {
        Name = name;
        Id = id;
    }

    public long Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
}

