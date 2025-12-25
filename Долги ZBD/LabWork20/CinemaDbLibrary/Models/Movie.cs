using System;
using System.Collections.Generic;

namespace CinemaDbLibrary.Models;

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

    public virtual ICollection<Frame> Frames { get; set; } = new List<Frame>();
}
