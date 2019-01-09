using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodiePal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodiePal.Controllers
{
    public class MainPageController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MainPageController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        // Shows default view and reads cookie if it exists.
        public IActionResult ClearCookie(string key)
        {
            CookieHelper cookieHelper = new CookieHelper(_httpContextAccessor, Request,
                                                         Response);
            cookieHelper.Remove(key);
            return RedirectToAction("Index", "Home");
        }

        // Let’s user store value in cookie.
        [HttpPost]
        public ActionResult Index(SiteUserVM siteUser)
        {
            CookieHelper cookieHelper = new CookieHelper(_httpContextAccessor, Request,
                                                         Response);
            cookieHelper.Set("firstName", siteUser.firstName, 1);
            // Redirect to GET method so cookie is read.
            return RedirectToAction("Index", "Home");
        }

        // Shows default view and reads cookie if it exists.
        [HttpGet]
        public IActionResult Index()
        {
            CookieHelper cookieHelper = new CookieHelper(_httpContextAccessor, Request,
                                                         Response);
            string firstName = cookieHelper.Get("firstName");
            if (firstName != null)
            {
                ViewBag.UserName = firstName;
            }
            return View();
        }

    }
}