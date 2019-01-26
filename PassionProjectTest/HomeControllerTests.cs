using FoodiePal.Repositories;
using FoodiePal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var controller = new HomeController(dbContext, new CuisinResturantVMRepo(dbContext) );
            int expected = 2;
            var viewResult = Assert.IsType<ViewResult>(controller.Index("name_asc", "", 1));
            var model = Assert.IsType<PaginatedList<CuisinResturantVM>>(viewResult.Model);
            int actual = model.Count;
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void IndexViewSearch()
        {
            var dbContext = new FoodiePalContext();
            var controller = new HomeController(dbContext, new CuisinResturantVMRepo(dbContext));
            string expected = "keyword";
            var viewResult = Assert.IsType<ViewResult>(controller.Index("name_asc", expected, 1));
            var model = Assert.IsType<PaginatedList<CuisinResturantVM>>(viewResult.Model);
            foreach (CuisinResturantVM m in model)
            {
                Assert.Contains(expected, m.Resturant.RestName);
            }
        }

        [Fact]
        public void UnitTestPageButton()
        {

            using (var context = new FoodiePalContext(DbOptionsFactory.DbContextOptions))
            {

                var mockCuiResVMRepo = new Mock<ICuisinResturantVMRepo>();
                mockCuiResVMRepo.Setup(mdb => mdb.getAll("name_asc", ""))
                    .Returns(new List<CuisinResturantVM> { new CuisinResturantVM(), new CuisinResturantVM(), new CuisinResturantVM()
                    }.AsQueryable());
                //var mockDb = new Mock<FoodiePalContext>();
                var controller = new HomeController(context, mockCuiResVMRepo.Object);
                var viewResult = Assert.IsType<ViewResult>(controller.Index("name_asc", "", 1));
                var model = Assert.IsType<PaginatedList<CuisinResturantVM>>(viewResult.Model);
                bool actual = model.HasNextPage;
                bool expected = true;
                Assert.Equal(actual, expected);
            }
        }


    }
}
