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
    public class ActrorsRolesController : Controller
    {
        private readonly DBCinemaContext _context;

        public ActrorsRolesController(DBCinemaContext context)
        {
            _context = context;
        }

        // GET: ActrorsRoles
           public async Task<IActionResult> Index()
           {
               var dBCinemaContext = _context.ActrorsRoles.Include(a => a.Actors).Include(a => a.Roles);
               return View(await dBCinemaContext.ToListAsync());
           }
        

        // GET: ActrorsRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actrorsRole = await _context.ActrorsRoles
                .Include(a => a.Actors)
                .Include(a => a.Roles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actrorsRole == null)
            {
                return NotFound();
            }

            return View(actrorsRole);
        }

        // GET: ActrorsRoles/Create
        public IActionResult Create(int? roleID, string roleName)
        {
            ViewData["ActorsId"] = new SelectList(_context.Actors, "Id", "Name");
            ViewData["RolesId"] = new SelectList(_context.Roles, "Id", "Name");
            ViewBag.RolesID = roleID;
            ViewBag.RolesName = roleName;
            return View();
        }

        // POST: ActrorsRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActorsId,RolesId")] ActrorsRole actrorsRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actrorsRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActorsId"] = new SelectList(_context.Actors, "Id", "Name", actrorsRole.ActorsId);
            ViewData["RolesId"] = new SelectList(_context.Roles, "Id", "Name", actrorsRole.RolesId);
            return View(actrorsRole);
        }

        // GET: ActrorsRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actrorsRole = await _context.ActrorsRoles.FindAsync(id);
            if (actrorsRole == null)
            {
                return NotFound();
            }
            ViewData["ActorsId"] = new SelectList(_context.Actors, "Id", "Name", actrorsRole.ActorsId);
            ViewData["RolesId"] = new SelectList(_context.Roles, "Id", "Name", actrorsRole.RolesId);
            return View(actrorsRole);
        }

        // POST: ActrorsRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ActorsId,RolesId")] ActrorsRole actrorsRole)
        {
            if (id != actrorsRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actrorsRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActrorsRoleExists(actrorsRole.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActorsId"] = new SelectList(_context.Actors, "Id", "Name", actrorsRole.ActorsId);
            ViewData["RolesId"] = new SelectList(_context.Roles, "Id", "Name", actrorsRole.RolesId);
            return View(actrorsRole);
        }

        // GET: ActrorsRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actrorsRole = await _context.ActrorsRoles
                .Include(a => a.Actors)
                .Include(a => a.Roles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actrorsRole == null)
            {
                return NotFound();
            }

            return View(actrorsRole);
        }

        // POST: ActrorsRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actrorsRole = await _context.ActrorsRoles.FindAsync(id);
            _context.ActrorsRoles.Remove(actrorsRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActrorsRoleExists(int id)
        {
            return _context.ActrorsRoles.Any(e => e.Id == id);
        }
    }
}
