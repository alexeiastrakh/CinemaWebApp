using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaWebApplication
{
    public partial class Actor
    {
        public Actor()
        {
            ActrorsRoles = new HashSet<ActrorsRole>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "The field must not be empty")]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; } = null!;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [StringLength(20, ErrorMessage = "The name must contain no more than 20 characters.")]
        public string? Info { get; set; }

        public virtual ICollection<ActrorsRole> ActrorsRoles { get; set; }
    }
}
