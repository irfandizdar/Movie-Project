using System;
using System.Collections.Generic;

namespace Movie_Backend.Models;

public partial class Series
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime? FirstAirDate { get; set; }

    public string? Creator { get; set; }

    public string? Description { get; set; }

    public decimal? Rating { get; set; }

    public int? NumberOfSeasons { get; set; }

    public string? PosterUrl { get; set; }

    public int? GenreId { get; set; }

    public virtual Genre? Genre { get; set; }

    //public string VideoUrl { get; set; }
}
