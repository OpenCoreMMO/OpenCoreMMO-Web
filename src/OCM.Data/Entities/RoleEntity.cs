using System;
using System.Collections.Generic;

namespace OCM.Infrastructure.Entities;

public class RoleEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Navigation property for accounts with this role
    public ICollection<AccountEntity> Accounts { get; set; }
}