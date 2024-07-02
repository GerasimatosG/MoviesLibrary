using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoviesLibrary.Models;

namespace MoviesLibrary.Controllers
{
    public class ActorController : Controller
    {
        private readonly MoviesLibraryCContext _context;

        public ActorController(MoviesLibraryCContext context)
        {
            _context = context;
        }    

        

        // GET: Actor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Firstname,Lastname,Gender,Birthdate")] Actor actor)
        {
            if (actor.Birthdate.HasValue && actor.Birthdate.Value.Kind != DateTimeKind.Utc)
            {
                actor.Birthdate = actor.Birthdate.Value.ToUniversalTime();
            }

            if (ModelState.IsValid)
            {                
                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction("ViewAll");
            }
            return RedirectToAction("ViewAll");
        }

        // GET: Actor/ViewAll/
        public IActionResult ViewAll()
        {
            var allActors = _context.Actors.OrderBy(c => c.Id).ToList();
            return View(allActors);
        }

        // GET: Actor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // POST: Actor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Firstname,Lastname,Gender,Birthdate")] Actor actor)
        {
            if (actor.Birthdate.HasValue && actor.Birthdate.Value.Kind != DateTimeKind.Utc)
            {
                actor.Birthdate = actor.Birthdate.Value.ToUniversalTime();
            }

            if (id != actor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("ViewAll");
            }
            return RedirectToAction("ViewAll");
        }

        // GET: Actor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor != null)
            {
                _context.Actors.Remove(actor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ViewAll");
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
    }
}
