using CarRentalApplication.Models.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models.ViewModels.Reservation
{
    public class ReservationContactViewModel : RegisterViewModel, IFormProcessing
    {
        public static string SessionKey = "rcvm";
        public new string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public new string ConfirmPassword { get; set; }
        //Use the RegisterViewModel validation
        public FormSubmissionViewModel FormProcessing { get; set; }

        
    }
}
