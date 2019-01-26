using FoodiePal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace FoodiePal.Repositories
{
    public class CuisinResturantVMRepo : ICuisinResturantVMRepo
    {
        private FoodiePalContext db;
        public CuisinResturantVMRepo(FoodiePalContext db)
        {
            this.db = db;
        }

        public IQueryable<CuisinResturantVM> getAll(string sortOrder, string searchString)
        {
            IQueryable<CuisinResturantVM> rcList;
            if (!String.IsNullOrEmpty(searchString))
            {
                rcList = db.Resturant.Where(r => r.RestName==searchString).Select(r => new CuisinResturantVM()
                {
                    Resturant = r,
                    CuisinNames = r.ResturantCuisin.Select(rc => rc.CuiName)
                });
            }
            else
            {
                rcList = db.Resturant.Select(r => new CuisinResturantVM()
                {
                    Resturant = r,
                    CuisinNames = r.ResturantCuisin.Select(rc => rc.CuiName)
                });
            }
            switch (sortOrder)
            {
                case "cuisin_asc":
                    rcList =
                        rcList.OrderBy(r => r.CuisinNames);
                    break;
                case "title_desc":
                    rcList =
                        rcList.OrderByDescending(r => r.Resturant.RestName);
                    break;
                case "cuisin_desc":
                    rcList =
                  rcList.OrderByDescending(r => r.CuisinNames);
                    break;
                default:
                    rcList =
                        rcList.OrderBy(r => r.Resturant.RestName);
                    break;
            };
            return rcList;
        }

        public CuisinResturantVM Get(int id)
        {
            CuisinResturantVM cr = db.Resturant.Where(r => r.RestId == id)
                .Select(r => new CuisinResturantVM()
                {
                    Resturant = r,
                    CuisinNames = r.ResturantCuisin.Select(rc => rc.CuiName)
                }).FirstOrDefault();
            return cr;
        }
    }
}
