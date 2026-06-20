using System;
using System.Collections.Generic;
using System.Text;

namespace DSW2026Ej15.Domain.Source;

public abstract class BaseEntity
{
    public Guid Id { get; init; }

    protected BaseEntity(Guid? id=null)
    {
        Id = id ?? Guid.NewGuid();
    }

}
