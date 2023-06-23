﻿namespace BankRateAggregator.Domain.Common;

public abstract class BaseEntity<TKey> : BaseAuditableEntity
{
    public TKey Id { get; set; }
}
