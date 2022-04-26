#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaWebApplication;


namespace CinemaWebApplication.Controllers
{
    
    public class MoviesController : Controller
    {
        private readonly DBCinemaContext _context;

        public MoviesController(DBCinemaContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(int? id, string? name, string? info)
        {
            if (id == null) return RedirectToAction("Index", "Countries");

            ViewBag.CountryId = id;
            ViewBag.CountryName = name;
            ViewBag.CountryInfo = info;

            var countriesByMovie = _context.Movies.Where(c => c.CountryId == id).Include(c => c.Country);
            return View(await countriesByMovie.ToListAsync());
        }
    
        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create(int countryId)
        {
            // ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name"); 
            ViewBag.CountryId = countryId;
           
            ViewBag.CountryInfo = _context.Countries.FirstOrDefault(t => t.Id == countryId)?.Info;
            ViewBag.CountryName = _context.Countries.FirstOrDefault(t => t.Id == countryId)?.Name;
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int countryId, [Bind("Id,Name,DateofRealese,CountryId,Info")] Movie movie)
        {
            movie.CountryId = countryId;
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                //  return RedirectToAction(nameof(Index)); 
                return RedirectToAction("Index", "Movies", new
                {
                    id = countryId,
                    name = _context.Countries.FirstOrDefault(t => t.Id == countryId)?.Name,
                    info = _context.Countries.FirstOrDefault(t => t.Id == countryId)?.Info
                });
            }
            // return View(movie); 
            return RedirectToAction("Index", "Movies", new
            {
                id = countryId,
                name = _context.Countries.FirstOrDefault(t => t.Id == countryId)?.Name,
                info = _context.Countries.FirstOrDefault(t => t.Id == countryId)?.Info
            });
        }
        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", movie.CountryId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateofRealese,Info,CountryId")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Movies", new
                    {
                        id = movie.CountryId,
                        name = _context.Countries.FirstOrDefault(t => t.Id == movie.CountryId)?.Name,
                        info = _context.Countries.FirstOrDefault(t => t.Id == movie.CountryId)?.Info
                    });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
             //   return RedirectToAction(nameof(Index));
            }
            //     ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", movie.CountryId);
            //  return View(movie);
            return RedirectToAction("Index", "Movies", new
            {
                id = movie.CountryId,
                name = _context.Countries.FirstOrDefault(t => t.Id == movie.CountryId)?.Name,
                info = _context.Countries.FirstOrDefault(t => t.Id == movie.CountryId)?.Info
            });
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            //   return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "Movies", new
            {
                id = movie.CountryId,
                name = _context.Countries.FirstOrDefault(t => t.Id == movie.CountryId)?.Name,
                info = _context.Countries.FirstOrDefault(t => t.Id == movie.CountryId)?.Info
            });
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

    }
}
