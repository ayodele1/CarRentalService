using CarRentalApplication.Models.ViewModels.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models = CarRentalApplication.Models;

namespace CarRentalApplication.Models.ViewModels.Dashboard
{
    public class ReservationsListViewModel : IFormProcessing
    {
        public FormSubmissionViewModel FormProcessing { get; set; }

        public IEnumerable<ReservationViewModel> UserReservations { get; set; }

        /// <summary>
        /// Reservation Selected by the User to be Updated
        /// </summary>
        public long ReservationId { get; set; }
    }
}
