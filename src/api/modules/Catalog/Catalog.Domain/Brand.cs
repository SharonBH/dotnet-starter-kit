﻿using FSH.Framework.Core.Domain;
using FSH.Framework.Core.Domain.Contracts;
using FSH.Starter.WebApi.Catalog.Domain.Events;

namespace FSH.Starter.WebApi.Catalog.Domain;
public class Brand : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    public string? LogoUrl { get; private set; } = string.Empty;

    private Brand() { }

    private Brand(Guid id, string name, string? description, string? logourl)
    {
        Id = id;
        Name = name;
        Description = description;
        LogoUrl = logourl;
        QueueDomainEvent(new BrandCreated { Brand = this });
    }

    public static Brand Create(string name, string? description, string? logourl)
    {
        return new Brand(Guid.NewGuid(), name, description, logourl);
    }

    public Brand Update(string? name, string? description)
    {
        bool isUpdated = false;

        if (!string.IsNullOrWhiteSpace(name) && !string.Equals(Name, name, StringComparison.OrdinalIgnoreCase))
        {
            Name = name;
            isUpdated = true;
        }

        if (!string.Equals(Description, description, StringComparison.OrdinalIgnoreCase))
        {
            Description = description;
            isUpdated = true;
        }

        if (isUpdated)
        {
            QueueDomainEvent(new BrandUpdated { Brand = this });
        }

        return this;
    }
}


