using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models.ViewModels.Reservation
{
    public class ReservationViewModel
    {
        public ReservationLogisticsViewModel LogisticsSetup { get; set; }

        public ReservationVehicleViewModel VehicleSetup { get; set; }

        public ReservationContactViewModel ContactSetup { get; set; }
    }
}
