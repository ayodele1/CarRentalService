using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models.ViewModels.Reservation
{
    public class ReservationViewModel : IFormProcessing
    {
        public ReservationLogisticsViewModel LogisticsSetup { get; set; }

        public ReservationVehicleViewModel VehicleSetup { get; set; }

        public ReservationContactViewModel ContactSetup { get; set; }

        public double StateTax
        {
            get { return 24.49; }
        }

        public double FederalTax
        {
            get { return 14.29; }
        }

        public FormSubmissionViewModel FormProcessing { get; set; }
    }
}
