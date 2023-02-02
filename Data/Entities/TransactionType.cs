﻿using Domain.Common;

namespace Domain.Entities;

public class TransactionType : Entity {
    public required string Name { get; set; }
    public required string Code { get; set; }

    // add constructor with validation, ef will auto-bind
}
