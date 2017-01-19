using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models.ViewModels.Reservation
{
    public class ReservationViewModel : IFormProcessing
    {
        public static string SessionKey = "rvm";
        private ReservationContactViewModel _contactSetup;
        private ReservationVehicleViewModel _vehicleSetup;
        private ReservationLogisticsViewModel _logisticsSetup;
        public ReservationLogisticsViewModel LogisticsSetup
        {
            get { return _logisticsSetup; }
            set
            {
                if (_logisticsSetup != null)
                {
                    value.CheckIfModelIsDirty(_logisticsSetup);                    
                }
                _logisticsSetup = value;
            }
        }
        public ReservationVehicleViewModel VehicleSetup
        {
            get { return _vehicleSetup; }
            set
            {
                if (_vehicleSetup != null)
                {
                    value.CheckIfModelIsDirty(_vehicleSetup);
                }
                _vehicleSetup = value;
            }
        }

        public ReservationContactViewModel ContactSetup
        {
            get { return _contactSetup; }
            set
            {
                if (_contactSetup != null)
                {
                    value.CheckIfModelIsDirty(_contactSetup);
                }
                _contactSetup = value;
            }
        }

        public long ConfirmationNumber { get; set; }

        public int TotalRentalDays
        {
            get { return Convert.ToInt32((this.LogisticsSetup.ReturnDate - this.LogisticsSetup.PickupDate).TotalDays); }
        }

        public double TotalVehicleCostMinusTax
        {
            get
            {
                if (VehicleSetup != null)
                {
                    if (VehicleSetup.Vehicle != null)
                    {
                        if (this.TotalRentalDays > 0)
                        {
                            return (this.VehicleSetup.Vehicle.PricePerDay * TotalRentalDays);
                        }
                        return this.VehicleSetup.Vehicle.PricePerDay;
                    }
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

        public double TotalCost
        {
            get { return this.TotalVehicleCostMinusTax + FederalTax + StateTax; }
        }

        public FormSubmissionViewModel FormProcessing { get; set; }

        public AppUser ApplicationUser { get; set; }

        public ReservationContact ReservationContact { get; set; }
    }
}
