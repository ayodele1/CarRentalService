export interface IReservation {
    confirmationNumber: number;
    userLocation: string;
    PickupLocation: string;
    ReturnLocation: string;
    PickupDate: Date;
    ReturnDate: Date;
    AppUserId: string;
    VehicleId: number;
    TotalCost: number;
    StateTax: number;
    FederalTax: number;
}