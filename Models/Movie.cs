using System;
using System.Collections.Generic;

namespace CinemaWebApplication
{
    public partial class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? Dateofrelease { get; set; }
        public string? Info { get; set; }
    }
}
