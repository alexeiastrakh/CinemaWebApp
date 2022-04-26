using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CinemaWebApplication;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CinemaWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly DBCinemaContext _context;
        public ChartsController(DBCinemaContext context)
        {
            _context = context;
        }

        [HttpGet("JsonDataCountries")]
        public JsonResult JsonDataCountries()
        {
            var countries = _context.Countries.Include(c => c.Movies).ToList();
            var countryMovie = new List<object> { new[] { "Country", "Amount of movies" } };
            countryMovie.AddRange(countries.Select(t => new object[] { t.Name, t.Movies.Count }));
            return new JsonResult(countryMovie);
        }
        [HttpGet("JsonDataRoles")]
        public JsonResult JsonDataRoles()
        {
            var roles = _context.Roles.Include(c => c.ActrorsRoles).ToList();
            var actorsrol = new List<object> { new[] { "Roles", "Amount of movies" } };
            actorsrol.AddRange(roles.Select(t => new object[] { t.Name, t.ActrorsRoles.Count }));
            return new JsonResult(actorsrol);
        }





    }
}
