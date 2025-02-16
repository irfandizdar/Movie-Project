using System;
using System.Collections.Generic;

namespace Movie_Backend.Models
{
    public partial class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? ReleaseDate { get; set; }
        public string? Director { get; set; }
        public string? Description { get; set; }
        public decimal? Rating { get; set; }
        public int? DurationMinutes { get; set; }
        public string? PosterUrl { get; set; }

        // Dodavanje veze na žanr
        public int? GenreId { get; set; }
        public virtual Genre? Genre { get; set; }

        // Veza sa recenzijama
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public string VideoUrl { get; set; }
    }
}
