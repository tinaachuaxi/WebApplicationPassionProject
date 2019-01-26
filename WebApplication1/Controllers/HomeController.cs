using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FoodiePal.Models;
using FoodiePal.Repositories;
using FoodiePal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private FoodiePalContext db;
        private ICuisinResturantVMRepo repo;

        public HomeController(FoodiePalContext db, ICuisinResturantVMRepo repo)
        {
            this.db = db;
            this.repo = repo;
        }
        
        public IActionResult Index(string sortOrder, string searchString, int? page)
        {
            string sort = String.IsNullOrEmpty(sortOrder) ? "name_asc" : sortOrder;
            string search = String.IsNullOrEmpty(searchString) ? "" : searchString;

            ViewData["CurrentSort"] = sort;
            ViewData["CurrentFilter"] = search;
            
            var resturants = repo.getAll(sort, search);
            int pageSize = 2;

            return View(PaginatedList<CuisinResturantVM>.Create(resturants
                       , page ?? 1, pageSize));
        }

        public IActionResult Details(int id)
        {
            var resturant = new ResturantRepo(db).get(id);
            return View(resturant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id)
        {
            var resturant = new ResturantRepo(db).get(id);
            return View(resturant);
        }
        
        public IActionResult Delete(int id)
        {
            var resturant = new ResturantRepo(db).get(id);
            return View(resturant);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resturant = await db.Resturant.FindAsync(id);
            db.Resturant.Remove(resturant);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RestName","RestAddress","PhoneNumber","Email")] Resturant resturant)
        {
            if (ModelState.IsValid)
            {
                db.Add(resturant);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resturant);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
