using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaWebApplication
{
    public partial class Producer
    {
        public Producer()
        {
            MoviesProducers = new HashSet<MoviesProducer>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "The field must not be empty")]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        [StringLength(20, MinimumLength = 3)]
        public string? Info { get; set; }

        public virtual ICollection<MoviesProducer> MoviesProducers { get; set; }
    }
}
