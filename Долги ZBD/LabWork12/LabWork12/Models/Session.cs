using System;
using System.Collections.Generic;

namespace LabWork12.Models;

public partial class Session
{
    public int SessionId { get; set; }

    public int MovieId { get; set; }

    public byte HallId { get; set; }

    public decimal Price { get; set; }

    public DateTime StartDate { get; set; }

    public bool Is3d { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
