using System;
using System.Collections.Generic;

namespace Movie_Backend.Models;

public partial class Actor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    public string? Country { get; set; }

    public string? Biography { get; set; }

    public string? ProfileImageUrl { get; set; }
}
