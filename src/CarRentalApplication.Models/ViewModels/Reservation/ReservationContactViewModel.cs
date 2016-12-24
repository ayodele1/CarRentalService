using CarRentalApplication.Models.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models.ViewModels.Reservation
{
    public class ReservationContactViewModel : RegisterViewModel, IFormProcessing
    {
        //Use the RegisterViewModel validation
        public FormSubmissionViewModel FormProcessing { get; set; }
    }
}
