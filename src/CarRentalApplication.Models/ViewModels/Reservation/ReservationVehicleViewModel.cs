using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models.ViewModels.Reservation
{
    public class ReservationVehicleViewModel : IFormProcessing
    {
        private bool _isDirty = false;
        private Vehicle _vehicle = null;
        public static string SessionKey = "rvvm";

        public Guid VehicleId { get; set; }
        public Vehicle Vehicle
        {
            get { return _vehicle; }
            set
            {
                _vehicle = value;
                if (value.Id != _vehicle.Id)
                    _isDirty = true;
            }
        }

        public IEnumerable<Vehicle> AvailableVehicles { get; set; }
        public bool IsDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }
        }
        public FormSubmissionViewModel FormProcessing { get; set; }
    }
}
