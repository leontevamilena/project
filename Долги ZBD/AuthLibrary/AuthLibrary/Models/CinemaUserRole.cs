using System;
using System.Collections.Generic;

namespace AuthLibrary.Models;

public partial class CinemaUserRole
{
    public int UserRoleId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CinemaUser> CinemaUsers { get; set; } = new List<CinemaUser>();

    public virtual ICollection<CinemaPrivilege> Privileges { get; set; } = new List<CinemaPrivilege>();
}
