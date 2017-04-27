using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models.ViewModels.Reservation
{
    public class ReservationVehicleViewModel : IFormProcessing
    {
        private bool _isDirty = false;

        public static string SessionKey = "rvvm";

        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public IEnumerable<Vehicle> AvailableVehicles { get; set; }

        public FormSubmissionViewModel FormProcessing { get; set; }

        public bool IsDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }
        }

        public void SetIsDirtyProperty(ReservationVehicleViewModel origVehicleSetup)
        {
            if(origVehicleSetup.VehicleId != this.VehicleId ||
                origVehicleSetup.Vehicle.Id != this.Vehicle.Id)
            {
                _isDirty = true;
            }
            else
            {
                _isDirty = false;
            }
        }
    }
}
