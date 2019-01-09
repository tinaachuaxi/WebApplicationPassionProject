using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class ResturantCuisin
    {
        public int RestId { get; set; }
        public string CuiName { get; set; }

        public Cuisin CuiNameNavigation { get; set; }
        public Resturant Rest { get; set; }
    }
}
