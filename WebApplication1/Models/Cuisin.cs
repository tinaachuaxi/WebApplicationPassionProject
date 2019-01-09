using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Cuisin
    {
        public Cuisin()
        {
            ResturantCuisin = new HashSet<ResturantCuisin>();
        }

        public string CuiName { get; set; }
        public string Descript { get; set; }

        public ICollection<ResturantCuisin> ResturantCuisin { get; set; }
    }
}
