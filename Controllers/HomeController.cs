using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using finalTicket.Models;

namespace finalTicket.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        if (User.IsInRole("Admin"))
        {
            return RedirectToAction("AdminDashboard", "Dashboard");
        }

        if (User.IsInRole("User"))
        {
            return RedirectToAction("UserDashboard", "Dashboard");
        }

        return View();
    }
}
