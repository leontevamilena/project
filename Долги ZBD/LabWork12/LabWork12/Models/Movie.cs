using System;
using System.Collections.Generic;

namespace LabWork12.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string Name { get; set; } = null!;

    public short Duration { get; set; }

    public short Year { get; set; }

    public string? Description { get; set; }

    public byte[]? Poster { get; set; }

    public string? AgeRating { get; set; }

    public DateOnly? StartRental { get; set; }

    public DateOnly? EndRental { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
