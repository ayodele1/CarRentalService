using CarRentalApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.ViewComponents
{
    public class YesNoModalViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(FormSubmissionViewModel fsvm)
        {
            return View(fsvm);
        }
    }
}
