using CarRentalApplication.Models;

namespace CarRentalApplication.Repositories
{
    public interface IReservationContactRepository
    {
        ReservationContact CreateNew(ReservationContact rc);
        ReservationContact FindByEmail(string email);
        bool IsExistingInDB(ReservationContact rc);
    }
}