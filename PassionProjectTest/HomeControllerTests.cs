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
        /*
         * Intergration test:
         * Check if the ascending sorting order works properly
         * only check for the first page currently
         */
        [Fact]
        public void IntergrationTestAscSort()
        {
            var context = new FoodiePalContext();
            var controller = new HomeController(context, new CuisinResturantVMRepo(context));
            var viewResult = Assert.IsType<ViewResult>(controller.Index("name_asc", "", 1));
            var model = Assert.IsType<PaginatedList<CuisinResturantVM>>(viewResult.Model);
            string[] expected = new string[] { "A&W", "Amigo Grill" };
            for (int i = 0; i < expected.Length; i++)
            {
                var actual = model[i].Resturant.RestName;
                Assert.Equal(expected[i], actual);
            }

        }

        /*
         * Intergration test:
         * check through each of the items in the first page if they match the current
         * searching key words
         */
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
                var actual = m.Resturant.RestName;
                Assert.Contains(expected, actual);
            }
        }


        /*
         * Unit test to test when it is not the last page, it suppose
         * to show the next page button
         */
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
        
        /*
         * Used to test when it get to the last page, it suppose
         * to hide the next page button
         * Should change to a intergration test
         */
        [Fact]
        public void UnitTestNotHasNextPage()
        {
            using (var context = new FoodiePalContext(DbOptionsFactory.DbContextOptions))
            {
                var mockCuiResVMRepo = new Mock<ICuisinResturantVMRepo>();
                mockCuiResVMRepo.Setup(mdb => mdb.getAll("name_asc", ""))
                    .Returns(new List<CuisinResturantVM> { new CuisinResturantVM(), new CuisinResturantVM()
                    }.AsQueryable());
                //var mockDb = new Mock<FoodiePalContext>();
                var controller = new HomeController(context, mockCuiResVMRepo.Object);
                var viewResult = Assert.IsType<ViewResult>(controller.Index("name_asc", "", 1));
                var model = Assert.IsType<PaginatedList<CuisinResturantVM>>(viewResult.Model);
                bool actual = model.HasNextPage;
                bool expected = false;
                Assert.Equal(actual, expected);
            }
        }

        /*
         * Unit Test:
         * Test the first page is showing 2 items
         */
        [Fact]
        public void UnitTestHas2ItemsAtTheFirstPage()
        {
            using (var context = new FoodiePalContext(DbOptionsFactory.DbContextOptions))
            {
                var mockCuiResVMRepo = new Mock<ICuisinResturantVMRepo>();
                mockCuiResVMRepo.Setup(mdb => mdb.getAll("name_asc", ""))
                    .Returns(new List<CuisinResturantVM> { new CuisinResturantVM(), new CuisinResturantVM()
                    }.AsQueryable());
                var controller = new HomeController(context, mockCuiResVMRepo.Object);
                int expected = 2;
                var viewResult = Assert.IsType<ViewResult>(controller.Index("name_asc", "", 1));
                var model = Assert.IsType<PaginatedList<CuisinResturantVM>>(viewResult.Model);
                int actual = model.Count;
                Assert.Equal(expected, actual);

            }
        }

        [Fact]
        public void UnitTestTheFinalPageDoesNotHave2Itmes()
        {
            using (var context = new FoodiePalContext(DbOptionsFactory.DbContextOptions))
            {
                var mockCuiResVMRepo = new Mock<ICuisinResturantVMRepo>();
                mockCuiResVMRepo.Setup(mdb => mdb.getAll("name_asc", ""))
                    .Returns(new List<CuisinResturantVM> { new CuisinResturantVM(), new CuisinResturantVM(),
                    new CuisinResturantVM()
                    }.AsQueryable());
                var controller = new HomeController(context, mockCuiResVMRepo.Object);
                int expected = 1;
                var viewResult = Assert.IsType<ViewResult>(controller.Index("name_asc", "", 2));
                var model = Assert.IsType<PaginatedList<CuisinResturantVM>>(viewResult.Model);
                int actual = model.Count;
                Assert.Equal(expected, actual);

            }
        }
    }
}
