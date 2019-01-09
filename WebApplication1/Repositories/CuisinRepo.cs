using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class CuisinRepo
    {
        FoodiePalContext db = new FoodiePalContext();

        public CuisinRepo(FoodiePalContext db)
        {
            this.db = db;
        }
        public IEnumerable<Cuisin> getAll(string sortOrder, string searchString)
        {
            IEnumerable<Cuisin> cuisin;/* = db.Cuisin.Select(c => c);*/
            if (!String.IsNullOrEmpty(searchString))
            {
                cuisin = db.Cuisin.Select(c => c).Where(c =>c.CuiName.Contains(searchString));
            }
            else
            {
                cuisin = db.Cuisin.Select(c => c);
            }

            return cuisin;
        }

        public Cuisin get(string id)
        {
            Cuisin cuisin = db.Cuisin.Where(c => c.CuiName == id).FirstOrDefault();
            return cuisin;
        }
    }
}
