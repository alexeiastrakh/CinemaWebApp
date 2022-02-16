using System;
using System.Collections.Generic;

namespace CinemaWebApplication
{
    public partial class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? DateofBirth { get; set; }
        public string? Info { get; set; }
        public string Surname { get; set; } = null!;
    }
}
