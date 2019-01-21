using FoodiePal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplication1.Controllers;
using WebApplication1.Models;
using Xunit;

namespace PassionProjectTest
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexViewHas2ItemsAtTheFirstPage()
        {
            var dbContext = new FoodiePalContext();
            var controller = new HomeController(dbContext);
            int expected = 2;
            var viewResult = Assert.IsType<ViewResult>(controller.Index("name_asc", "", 1));
            var model = Assert.IsType<PaginatedList<CuisinResturantVM>>(viewResult.Model);
            int actual = model.Count;
            Assert.Equal(expected, actual);

        }
    }
}
