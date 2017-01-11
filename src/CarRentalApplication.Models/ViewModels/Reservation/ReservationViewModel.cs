﻿using System;
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

        public int TotalRentalDays
        {
            get { return Convert.ToInt32((this.LogisticsSetup.ReturnDate - this.LogisticsSetup.PickupDate).TotalDays); }
        }

        public double TotalVehicleCostMinusTax
        {
            get
            {
                if (this.TotalRentalDays > 0)
                {
                    return (this.VehicleSetup.Vehicle.PricePerDay * TotalRentalDays);
                }
                return this.VehicleSetup.Vehicle.PricePerDay;
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

        public double TotalRentalCost
        {
            get { return this.TotalVehicleCostMinusTax + FederalTax + StateTax; }
        }

        public FormSubmissionViewModel FormProcessing { get; set; }
    }
}
