﻿namespace FC.Codeflix.Catalog.Domain;

public class Category
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Category(string name, string description, bool isActive = true)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = DateTime.Now;

        Validate();
    }   

    public void Validate()
    {
        if(string.IsNullOrWhiteSpace(Name))
            throw new EntityValidationException($"{nameof(Name)} should not be empty nor null");

        if(Name.Length < 3)
            throw new EntityValidationException($"{nameof(Name)} should be at least 3 characters");

        if(Description is null)
            throw new EntityValidationException($"{nameof(Description)} should not be null");
    }
}
