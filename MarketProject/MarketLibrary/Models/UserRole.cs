using System;
using System.Collections.Generic;

namespace MarketLibrary.Models;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
