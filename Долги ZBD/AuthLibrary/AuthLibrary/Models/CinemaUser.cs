using System;
using System.Collections.Generic;

namespace AuthLibrary.Models;

public partial class CinemaUser
{
    public int UserId { get; set; }

    public string Login { get; set; } = null!;

    public string HashPassword { get; set; } = null!;

    public int NumberAttempsPassword { get; set; }

    public DateTime? DateUnlock { get; set; }

    public int UserRoleId { get; set; }

    public virtual CinemaUserRole UserRole { get; set; } = null!;
}
