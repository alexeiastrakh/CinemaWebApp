using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaWebApplication
{
    public partial class Genre
    {
        public Genre()
        {
            MoviesGenres = new HashSet<MoviesGenre>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "The field must not be empty")]
        [StringLength(10, MinimumLength = 3)]
        public string Name { get; set; } = null!;
        [StringLength(20, MinimumLength = 3)]
        public string? Info { get; set; }

        public virtual ICollection<MoviesGenre> MoviesGenres { get; set; }
    }
}
