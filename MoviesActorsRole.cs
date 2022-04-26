using System;
using System.Collections.Generic;

namespace CinemaWebApplication
{
    public partial class MoviesActorsRole
    {
        public int Id { get; set; }
        public int MoviesId { get; set; }
        public int ActorsRolesId { get; set; }

        public virtual ActrorsRole ActorsRoles { get; set; } = null!;
        public virtual Movie Movies { get; set; } = null!;
    }
}
