using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaWebApplication
{
    public partial class Role
    {
        public Role()
        {
            ActrorsRoles = new HashSet<ActrorsRole>();
        }

        public int Id { get; set; }
        [StringLength(20, ErrorMessage = "The name must contain no more than 20 characters.")]
        public string Name { get; set; } = null!;

        public virtual ICollection<ActrorsRole> ActrorsRoles { get; set; }
    }
}
