using System;
using System.Collections.Generic;

namespace Movie_Backend.Models;

public partial class Genre
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; } = new List<Movie>();

    public virtual ICollection<Series> Series { get; } = new List<Series>();
}
