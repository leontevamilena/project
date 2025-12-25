using System;
using System.Collections.Generic;

namespace LabWork12.Models;

public partial class Visitor
{
    public int VisitorId { get; set; }

    public string Phone { get; set; } = null!;

    public string? Name { get; set; }

    public DateTime? BirthDay { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
