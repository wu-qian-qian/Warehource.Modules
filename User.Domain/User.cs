﻿using Common.Domain.EF;

namespace Identity.Domain;

public class User : IEntity
{
    public User() : base(Guid.NewGuid())
    {
    }

    public string Name { get; set; }

    public string? Description { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public DateTimeOffset LockoutEnd { get; set; }

    public Guid RoleId { get; set; }

    public Role? Role { get; set; }

    public bool CheckLockoutEnd()
    {
        return DateTime.Now > LockoutEnd;
    }

    public bool CheckLogin(string pwd)
    {
        return pwd == Password;
    }
}