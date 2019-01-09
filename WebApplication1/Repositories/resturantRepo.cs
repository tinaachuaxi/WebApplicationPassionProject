using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1
{
    public class ResturantRepo
    {
        FoodiePalContext db = new FoodiePalContext();

        public ResturantRepo(FoodiePalContext db)
        {
            this.db = db;
        }
        public IQueryable<Resturant> getAll(string sortOrder, string searchString)
        {
            IQueryable<Resturant> resturants;/* = db.Resturant.Select(r=>r);*/
            if(!String.IsNullOrEmpty(searchString))
            {
                resturants = db.Resturant.Select(r => r).Where(r => r.RestName.Contains(searchString));
            }
            else
            {
                resturants = db.Resturant.Select(r => r);
            }
            switch (sortOrder)
            {
                case "cuisin_asc":
                    resturants =
                        resturants.OrderBy(r => r.RestAddress);
                    break;
                case "title_desc":
                    resturants =
                        resturants.OrderByDescending(r => r.RestName);
                    break;
                case "cuisin_desc":
                    resturants =
                  resturants.OrderByDescending(r => r.RestAddress);
                    break;
                default:
                    resturants =
                        resturants.OrderBy(r => r.RestName);
                    break;
            };

            return resturants;
        }

        public Resturant get(int id)
        {
            Resturant resturant = db.Resturant.Where(r => r.RestId == id).FirstOrDefault();
            return resturant;
        }
        
        public Resturant update(int id, string restName, string restAddress, string phoneNumber, string email)
        {
            Resturant resturant = db.Resturant.Where(r => r.RestId == id).FirstOrDefault();
            resturant.RestName = restName;
            resturant.RestAddress = restAddress;
            resturant.PhoneNumber = phoneNumber;
            resturant.Email = email;
            db.SaveChanges();
            return resturant;
        }
    }
}
