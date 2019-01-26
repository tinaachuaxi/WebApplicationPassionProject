using FoodiePal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodiePal.Repositories
{
    public interface ICuisinResturantVMRepo
    {
        IQueryable<CuisinResturantVM> getAll(string sortOrder, string searchString);
        CuisinResturantVM Get(int id);

    }
}
