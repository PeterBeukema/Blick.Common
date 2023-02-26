﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Blick.Common.Store.Abstractions.Models;

public abstract class Entity
{
    [Key] public Guid Identifier { get; set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}