using System;
using System.Collections.Generic;

namespace CinemaWebApplication
{
    public partial class ActrorsRole
    {
        public ActrorsRole()
        {
            MoviesActorsRoles = new HashSet<MoviesActorsRole>();
        }

        public int Id { get; set; }
        public int ActorsId { get; set; }
        public int RolesId { get; set; }

        public virtual Actor Actors { get; set; } = null!;
        public virtual Role Roles { get; set; } = null!;
        public virtual ICollection<MoviesActorsRole> MoviesActorsRoles { get; set; }
    }
}
