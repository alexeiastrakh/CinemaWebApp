using System;
using System.Collections.Generic;

namespace CinemaWebApplication
{
    public partial class MoviesProducer
    {
        public int MoviesId { get; set; }
        public int ProducersId { get; set; }
        public int Id { get; set; }

        public virtual Movie Movies { get; set; } = null!;
        public virtual Producer Producers { get; set; } = null!;
    }
}
