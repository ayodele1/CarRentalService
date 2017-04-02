using CarRentalApplication.Models.ViewModels.Reservation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models.ViewModels.Api
{
    public class ApiReservationViewModel
    {
        public long ConfirmationNumber { get; set; }

        public int TotalRentalDays
        {
            get { return Convert.ToInt32((this.ReturnDate - this.PickupDate).TotalDays); }
        }

        public double TotalVehicleCostMinusTax
        {
            get
            {
                if (Vehicle != null)
                {
                    if (this.TotalRentalDays > 0)
                    {
                        return (this.Vehicle.PricePerDay * TotalRentalDays);
                    }
                    return this.Vehicle.PricePerDay;
                }
                return double.MinValue;
            }

        }
        public double StateTax
        {
            get { return 24.49; }
        }

        public double FederalTax
        {
            get { return 14.29; }
        }

        public Guid VehicleId { get; set; }

        public Guid ReservationContactId { get; set; }

        public Guid AppUserId { get; set; }

        public double TotalCost
        {
            get { return this.TotalVehicleCostMinusTax + FederalTax + StateTax; }
        }
        [Required(ErrorMessage = "Your Location is Required")]
        public string UserLocation { get; set; }

        [Required(ErrorMessage = "Please Select a Pickup Location")]
        public string PickupLocation { get; set; }

        [Required(ErrorMessage = "Please select a Return Location")]
        public string ReturnLocation { get; set; }

        [IsStoreOpen]
        public DateTime PickupDate { get; set; }

        [IsStoreOpen]
        [IsTimeDurationValid("PickupDate")]
        public DateTime ReturnDate { get; set; }

        public ApiVehicleViewModel Vehicle { get; set; }

        public ApiContactViewModel ReservationContact { get; set; }
    }
}
