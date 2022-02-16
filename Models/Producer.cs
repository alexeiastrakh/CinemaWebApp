using System;
using System.Collections.Generic;

namespace CinemaWebApplication
{
    public partial class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime? DateofBirth { get; set; }
        public string? Info { get; set; }
    }
}
