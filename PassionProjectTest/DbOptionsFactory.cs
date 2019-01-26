using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;
using System.IO;

namespace PassionProjectTest
{
   
    public static class DbOptionsFactory
    {
        static DbOptionsFactory()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Path.GetFullPath("../../../")))
                .AddJsonFile("testsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var connectionString = config["ConnectionStrings:FoodiePalConnection"];

            DbContextOptions = new DbContextOptionsBuilder<FoodiePalContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public static DbContextOptions<FoodiePalContext> DbContextOptions { get; }
    }

}
