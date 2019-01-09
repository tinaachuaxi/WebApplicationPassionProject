using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Resturant
    {
        public Resturant()
        {
            ResturantCuisin = new HashSet<ResturantCuisin>();
        }

        public int RestId { get; set; }
        public string RestName { get; set; }
        public string RestAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public ICollection<ResturantCuisin> ResturantCuisin { get; set; }
    }
}
