using CarRentalApplication.Models.ViewModels.Reservation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.ViewComponents
{
    public class ReservationUpdateViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ReservationViewModel rvm)
        {
            return View(rvm);
        }
    }
}
