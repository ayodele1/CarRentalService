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
        public IViewComponentResult Invoke(LoginViewModel lvm, FormSubmissionViewModel fsvm)
        {
            var tupleViewModel = new Tuple<LoginViewModel, FormSubmissionViewModel>(lvm, fsvm);          
            return View(tupleViewModel);
        }
    }
}
