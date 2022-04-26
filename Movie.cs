using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaWebApplication
{
    public partial class Movie
    {
        public Movie()
        {
            MoviesActorsRoles = new HashSet<MoviesActorsRole>();
            MoviesGenres = new HashSet<MoviesGenre>();
            MoviesProducers = new HashSet<MoviesProducer>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "The field must not be empty")]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; } = null!;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateofRealese { get; set; }
        [StringLength(20, ErrorMessage = "The name must contain no more than 20 characters.")]
        public string? Info { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<MoviesActorsRole> MoviesActorsRoles { get; set; }
        public virtual ICollection<MoviesGenre> MoviesGenres { get; set; }
        public virtual ICollection<MoviesProducer> MoviesProducers { get; set; }
    }
}
