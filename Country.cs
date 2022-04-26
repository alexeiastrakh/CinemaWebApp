using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaWebApplication
{
    public partial class Country
    {
        public Country()
        {
            Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "The field must not be empty")]
       
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; } = null!;
        [StringLength(50, MinimumLength = 3)]
        public string? Info { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
