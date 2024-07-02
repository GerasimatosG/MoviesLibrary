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
    public class CategoryController : Controller
    {
        
        private readonly MoviesLibraryCContext _context;

        public CategoryController(MoviesLibraryCContext context)
        {
            _context = context;
        }      
   

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Categoryname")] Category category)
        {
            if (ModelState.IsValid)
            {
                var allCategories = _context.Categories.ToList();
                var categoryExists = allCategories.Any(m => m.Categoryname == category.Categoryname );
                if (categoryExists == false)
                {
                    _context.Add(category);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("ViewAll");
                }
                else
                {
                    ViewBag.CategoryError = "Category Already Exists!!";
                }
            }
            else
            {
                ViewBag.CategoryError = "Could Not Add the Category";
            }
            var categories = _context.Categories.OrderBy(c => c.Id).ToList();
            return View("ViewAll", categories);
        }

        // GET: Category/Edit/5
        public IActionResult ViewAll()
        {
            var allCategories = _context.Categories.OrderBy(c => c.Id).ToList();
            return View(allCategories);
        }

        // POST: Category/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Categoryname")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {                
                var categoryExists = _context.Categories.Any(c => c.Id != category.Id && c.Categoryname == category.Categoryname);

                if (!categoryExists)
                {
                    try
                    {
                        _context.Update(category);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("ViewAll");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CategoryExists(category.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw; 
                        }
                    }
                }
                else
                {
                    ViewBag.CategoryError = "Cant Update Category Because New Category Already Exists!!";
                }
            }
            else
            {
                ViewBag.CategoryError = "Cant Update Category Because New Category Is Empty!!";
            }
            var categories = _context.Categories.OrderBy(c => c.Id).ToList();
            return View("ViewAll", categories);
        }


        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Failure");
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return View("Failure");
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ViewAll");
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
