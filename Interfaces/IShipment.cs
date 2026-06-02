using SwiftRoute_Courier___OOP_Assesment.Enums;
using SwiftRoute_Courier___OOP_Assesment.Models;

namespace SwiftRoute_Courier___OOP_Assesment.Interfaces
{
    interface IShipment
    {
        void AssignCourier(Courier courier);
        void EnableInsurance();
        void EnableCOD(decimal amount);
        List<decimal> GetAllPrices();
        decimal CalculateTotalPrice();
        void AdvanceStatus(ParcelStatus newStatus);
    }
}
