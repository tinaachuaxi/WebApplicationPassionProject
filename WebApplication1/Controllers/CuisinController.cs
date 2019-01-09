using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    public class CuisinController : Controller
    {
        private FoodiePalContext db;
    
        public CuisinController(FoodiePalContext db)
        {
            this.db = db;
        }

        public IActionResult Index(string sortOrder, string searchString)
        {
            string sort = String.IsNullOrEmpty(sortOrder) ? "name_asc" : sortOrder;
            string search = String.IsNullOrEmpty(searchString) ? "" : searchString;

            ViewData["CurrentSort"] = sort;
            ViewData["CurrentFilter"] = search;
            var cuisins = new CuisinRepo(db).getAll(sort, search);
            return View(cuisins);
        }

        public IActionResult Details(string id)
        {
            var cuisin = new CuisinRepo(db).get(id);
            return View(cuisin);
        }

        public IActionResult Delete(string id)
        {
            var cuisin = new CuisinRepo(db).get(id);
            return View(cuisin);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cuisin = await db.Cuisin.FindAsync(id);
            db.Cuisin.Remove(cuisin);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CuiName","Descript")] Cuisin cuisin)
        {
            if (ModelState.IsValid)
            {
                db.Add(cuisin);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cuisin);
        }
    }
}