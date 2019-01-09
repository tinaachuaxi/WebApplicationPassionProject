using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodiePal.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    public class ResturantCuisinController : Controller
    {
        private FoodiePalContext db;

        public ResturantCuisinController(FoodiePalContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            //var resturantCuisins = new CuisinResturantVMRepo(db).getAll();
            return View();
        }

        public IActionResult Details(int id)
        {
            var resturantCuisin = new CuisinResturantVMRepo(db).Get(id);
            return View(resturantCuisin);
        }

        public IActionResult Delete(string cuiName, int id)
        {
            var rc = new ResturantCuisinRepo(db).get(cuiName, id);
            return View(rc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var rc = await db.ResturantCuisin.FindAsync(id);
            db.ResturantCuisin.Remove(rc);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}