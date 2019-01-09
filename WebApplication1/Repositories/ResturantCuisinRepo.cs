using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class ResturantCuisinRepo
    {
        FoodiePalContext db = new FoodiePalContext();

        public ResturantCuisinRepo(FoodiePalContext db)
        {
            this.db = db;
        }
        public IEnumerable<ResturantCuisin> getAll()
        {
            IEnumerable<ResturantCuisin> resturantCuisins = db.ResturantCuisin.Select(c => c);
            return resturantCuisins;
        }

        public ResturantCuisin get(string cuiName, int id)
        {
            ResturantCuisin resturantCuisins = db.ResturantCuisin.Where(c => c.CuiName == cuiName && c.RestId==id).FirstOrDefault();
            return resturantCuisins;
        }
    }
}
