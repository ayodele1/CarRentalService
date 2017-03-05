using CarRentalApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Controllers.Dashboard
{
    public class DashboardController : Controller
    {
        public IActionResult Home()
        {            
            return View();
        }
    }
}
