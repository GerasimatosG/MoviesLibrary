using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesLibrary.Controllers
{
    public class PlatformController : Controller
    {
        private readonly MoviesLibraryCContext _context;

        public PlatformController(MoviesLibraryCContext context)
        {
            _context = context;
        }

        // GET: Platform/ViewAll
        public async Task<IActionResult> ViewAll()
        {
            var allPlatforms = await _context.Platforms.OrderBy(p => p.Id).ToListAsync();
            return View(allPlatforms);
        }

        // GET: Platform/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Platform/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Platformname")] Platform platform)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var platformExists = await _context.Platforms.AnyAsync(p => p.Platformname == platform.Platformname);
                    if (!platformExists)
                    {
                        _context.Add(platform);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("ViewAll");
                    }
                    else
                    {
                        ViewBag.PlatformError = "Platform Already Exists!!";
                    }
                }
                else
                {
                    ViewBag.PlatformError = "Could Not Add the Platform";
                }
            }
            catch (Exception ex)
            {
                ViewBag.PlatformError = $"Error: {ex.Message}";
            }

            var platforms = await _context.Platforms.OrderBy(p => p.Id).ToListAsync();
            return View("ViewAll", platforms);
        }

        // GET: Platform/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platform = await _context.Platforms.FindAsync(id);
            if (platform == null)
            {
                return NotFound();
            }

            return View(platform);
        }

        // POST: Platform/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Platformname")] Platform platform)
        {
            if (id != platform.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingPlatform = await _context.Platforms.FindAsync(id);

                    if (existingPlatform == null)
                    {
                        return NotFound();
                    }

                    var platformWithSameName = await _context.Platforms
                        .FirstOrDefaultAsync(p => p.Platformname == platform.Platformname && p.Id != platform.Id);

                    if (platformWithSameName != null)
                    {
                        ViewBag.PlatformError = "Platform name already exists.";
                        var platforms = await _context.Platforms.OrderBy(p => p.Id).ToListAsync();
                        return View("ViewAll", platforms);
                    }

                    existingPlatform.Platformname = platform.Platformname;

                    await _context.SaveChangesAsync();

                    return RedirectToAction("ViewAll");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatformExists(platform.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.PlatformError = "Could Not Update the Platform.";
            var allPlatforms = await _context.Platforms.OrderBy(p => p.Id).ToListAsync();
            return View("ViewAll", allPlatforms);
        }

        // GET: Platform/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platform = await _context.Platforms.FirstOrDefaultAsync(m => m.Id == id);
            if (platform == null)
            {
                return NotFound();
            }

            return View(platform);
        }

        // POST: Platform/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var platform = await _context.Platforms.FindAsync(id);
            if (platform != null)
            {
                _context.Platforms.Remove(platform);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ViewAll");
        }

        private bool PlatformExists(int id)
        {
            return _context.Platforms.Any(p => p.Id == id);
        }
    }
}
