using SwiftRoute_Courier___OOP_Assesment.Enums;
using SwiftRoute_Courier___OOP_Assesment.Models;

namespace SwiftRoute_Courier___OOP_Assesment.Interfaces
{
    interface IDispatcher
    {
        Shipment BookShipment(Parcel parcel, Customer customer, ServiceTier tier);
        void AssignCourier(Shipment shipment, Courier courier);
        void AdvanceStatus(Shipment shipment, ParcelStatus status);
        List<Shipment> GetInTransitShipments();
        (List<Shipment>, decimal Revenue) GetDeliveredTodayReport();
        (List<Shipment>, decimal) GenerateBusinessInvoice(BusinessCustomer customer);
        List<Shipment> GetAllShipments();
    }
}
