using SwiftRoute_Courier___OOP_Assesment.Enums;
using SwiftRoute_Courier___OOP_Assesment.Interfaces;

namespace SwiftRoute_Courier___OOP_Assesment.Models
{
    class Dispatcher : IDispatcher
    {
        private List<Shipment> _shipments = new List<Shipment>();

        public Shipment BookShipment(
        Parcel parcel,
        Customer customer,
        ServiceTier tier)
        {
            var shipment = new Shipment(
                parcel,
                customer,
                tier);

            _shipments.Add(shipment);

            return shipment;
        }

        public void AssignCourier(
            Shipment shipment,
            Courier courier)
        {
            shipment.AssignCourier(courier);
        }

        public void AdvanceStatus(
            Shipment shipment,
            ParcelStatus status)
        {
            shipment.AdvanceStatus(status);
        }

        public List<Shipment> GetInTransitShipments()
        {
            return _shipments
                .Where(x =>
                    x.Status == ParcelStatus.PickedUp ||
                    x.Status == ParcelStatus.InTransit ||
                    x.Status == ParcelStatus.OutForDelivery)
                .ToList();
        }

        public (List<Shipment>, decimal Revenue)
            GetDeliveredTodayReport()
        {
            var delivered = _shipments
                .Where(x =>
                    x.Status == ParcelStatus.Delivered &&
                    x.DeliveredAt.Date ==
                        DateTime.UtcNow.Date)
                .ToList();

            decimal revenue =
                delivered.Sum(x => x.CalculateTotalPrice());

            return (delivered, revenue);
        }

        public (List<Shipment>, decimal, decimal) GenerateBusinessInvoice(
            BusinessCustomer customer)
        {
            var delivered = _shipments
                .Where(x =>
                    x.Customer.Id == customer.Id &&
                    x.Status == ParcelStatus.Delivered)
                .ToList();

            var total = _shipments
                .Where(x =>
                    x.Customer.Id == customer.Id &&
                    x.Status == ParcelStatus.Delivered)
                .Sum(x => x.CalculateTotalPrice());

            var discount = 0.0m;

            if(customer is IDiscount)
            {
                discount = total * customer.GetDiscountRate();
            }

            return (delivered, discount, total);
        }

        public List<Shipment> GetAllShipments()
        {
            return _shipments.ToList();
        }
    }
}
