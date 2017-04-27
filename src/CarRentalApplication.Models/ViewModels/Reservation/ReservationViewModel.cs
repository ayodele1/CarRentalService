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
        private ReservationVehicleViewModel _origVehicleSetup;
        private ReservationLogisticsViewModel _origLogisticsSetup;
        private ReservationContactViewModel _origContactSetup;
        private ReservationLogisticsViewModel _logisticsSetup;
        private double _totalCost;
        private double _totalVehicleCost;
        public ReservationLogisticsViewModel LogisticsSetup
        {
            get { return _logisticsSetup; }
            set
            {
                if (_logisticsSetup != null)
                {
                    if (OrigLogisticsSetup == null) OrigLogisticsSetup = _logisticsSetup;
                    value.SetIsDirtyProperty(_logisticsSetup);                    
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
                    if (OrigVehicleSetup == null) OrigVehicleSetup = _vehicleSetup;
                    value.SetIsDirtyProperty(_origVehicleSetup);
                }
                _vehicleSetup = value;
            }
        }

        public ReservationLogisticsViewModel OrigLogisticsSetup
        {
            get { return _origLogisticsSetup; }
            set { _origLogisticsSetup = value; }
        }
        public ReservationContactViewModel OrigContactSetup
        {
            get { return _origContactSetup; }
            set { _origContactSetup = value; }
        }
        public ReservationVehicleViewModel OrigVehicleSetup
        {
            get { return _origVehicleSetup; }
            set { _origVehicleSetup = value; }
        }

        public ReservationContactViewModel ContactSetup
        {
            get { return _contactSetup; }
            set
            {
                if (_contactSetup != null)
                {
                    if (OrigContactSetup == null) OrigContactSetup = _contactSetup;
                    value.SetIsDirtyProperty(_contactSetup);
                }
                _contactSetup = value;
            }
        }

        public bool IsChanged
        {
            get
            {
                if(VehicleSetup != null && LogisticsSetup != null && ContactSetup != null)
                {
                    if (VehicleSetup.IsDirty || LogisticsSetup.IsDirty || ContactSetup.IsDirty)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public long ConfirmationNumber { get; set; }

        public int TotalRentalDays
        {
            get
            {
                if(LogisticsSetup != null)
                {
                    return Convert.ToInt32((this.LogisticsSetup.ReturnDate - this.LogisticsSetup.PickupDate).TotalDays);
                }
                return 0;         
            }
        }

        public double TotalVehicleCost
        {
            get { return _totalVehicleCost; }
            set { _totalVehicleCost = value; }
        }

        public void CalculateTotalVehicleCost(Vehicle v, int totalRentalDays)
        {
            _totalVehicleCost = double.MinValue;
            if (v != null)
            {
                _totalVehicleCost = (totalRentalDays > 0) ? v.PricePerDay * totalRentalDays : v.PricePerDay;
            }
            
        }

        public void CalculateTotalCost(Vehicle v, int totalRentalDays)
        {
            _totalCost = TotalVehicleCost + FederalTax + StateTax;
        }
        public double StateTax
        {
            get { return 24.49; }
        }

        public double Discount
        {
            get { return 0.00; }
        }

        public double FederalTax
        {
            get { return 14.29; }
        }

        public double TotalCost
        {
            get { return _totalCost; }
            set { _totalCost = value; }
        }

        public FormSubmissionViewModel FormProcessing { get; set; }

        public AppUser ApplicationUser { get; set; }

        public ReservationContact ReservationContact { get; set; }
    }
}
