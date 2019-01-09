using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace FoodiePal.ViewModels
{
    public class CuisinResturantVM
    {
        public Resturant Resturant { get; set; }
        public IEnumerable<String> CuisinNames { get; set; }
    }
}
