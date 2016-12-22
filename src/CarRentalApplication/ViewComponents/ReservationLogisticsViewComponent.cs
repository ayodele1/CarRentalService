using CarRentalApplication.Models.ViewModels.Reservation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.ViewComponents
{
    public class ReservationLogisticsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ReservationLogisticsViewModel rlvm)
        {
            return View(rlvm);
        }
    }
}
