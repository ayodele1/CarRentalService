using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models
{
    public class Reservation : IModificationHistory
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public long ConfirmationNumber { get; set; }

        //ReservationContact Foreign Key
        public Guid ReservationContactId { get; set; }

        //ReservationContact Navigation Property
        public ReservationContact ReservationContact { get; set; }

        //Identity User Foreign Key
        public string AppUserId { get; set; }

        //Application User Navigation Property
        public AppUser AppUser { get; set; }

        public Guid VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

        public double TotalCost { get; set; }        

        public double StateTax { get; set; }

        public double FederalTax { get; set; }

        public string UserLocation { get; set; }

        public string PickupLocation { get; set; }

        public string ReturnLocation { get; set; }

        public DateTime PickupDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
