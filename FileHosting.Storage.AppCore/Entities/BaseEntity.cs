﻿namespace FileHosting.Storage.AppCore.Entities;

public abstract class BaseEntity
{
    public virtual int Id { get; protected set; }
}