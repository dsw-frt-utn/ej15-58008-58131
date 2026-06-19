using System;
using System.Collections.Generic;
using System.Text;

namespace DSW2026Ej15.Domain;

public class Speciality : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Speciality(string name, string description, Guid id)
    {
        Name = name;
        Description = description;
        Id = id;
    }

    public Speciality(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
