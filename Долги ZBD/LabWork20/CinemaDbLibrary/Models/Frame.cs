using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaDbLibrary.Models;

public partial class Frame
{
    public int FrameId { get; set; }
    [Display(Name = "Movie")]
    public int MovieId { get; set; }

    public string FileName { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
