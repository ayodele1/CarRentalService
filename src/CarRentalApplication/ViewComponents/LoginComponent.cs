using CarRentalApplication.Models.ViewModels;
using CarRentalApplication.Models.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.ViewComponents
{
    public class LoginComponent : ViewComponent
    {
        public IViewComponentResult Invoke(LoginViewModel lvm)
        {       
            return View(lvm);
        }
    }
}
