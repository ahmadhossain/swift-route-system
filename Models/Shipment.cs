using SwiftRoute_Courier___OOP_Assesment.Enums;
using SwiftRoute_Courier___OOP_Assesment.Interfaces;

namespace SwiftRoute_Courier___OOP_Assesment.Models
{
    class Shipment : IShipment
    {
        private Courier _courier;
        public Parcel Parcel { get; private set; }
        public Customer Customer { get; private set; }
        public ServiceTier Tier { get; private set; }
        public ParcelStatus Status { get; private set; } = ParcelStatus.Booked;
        public bool IsInsuranceTaken { get; private set; }
        public bool IsCODEnabled { get; private set; }
        public DateTime DeliveredAt { get; private set; }

        public Shipment(Parcel parcel, Customer customer, ServiceTier tier)
        {
            Parcel = parcel;
            Customer = customer;
            Tier = tier;

            if (Parcel is IInsuranceable)
            {
                var insurance = Parcel as IInsuranceable;
                if (insurance.IsInsuranceMandatory())
                {
                    EnableInsurance();
                }
            }
        }

        public void AssignCourier(Courier courier)
        {
            if (Parcel.WeightKg > courier.MaxWeightLimit())
            {
                throw new Exception($"Weight cannot exceed {courier.MaxWeightLimit()}Kg!");
            }

            if (!courier.IsTierEligible(Tier))
            {
                throw new Exception("Service Tier is not allowed for this courier!");
            }

            _courier = courier;
        }

        public void EnableInsurance()
        {
            if (!(Parcel is IInsuranceable))
            {
                throw new Exception("Insurance is not available for this parcel type!");
            }

            if (Parcel.DeclaredValue == 0)
            {
                throw new Exception("Declared Value is not set!");
            }

            IsInsuranceTaken = true;
        }

        public void EnableCOD(decimal amount)
        {
            if (Customer is BusinessCustomer)
            {
                throw new Exception("Cash on Devilery is not available to business customer!");
            }

            if (!(Parcel is ICashOnDeliveryable))
            {
                throw new Exception("Cash on Devilery is not available to this Parcel Type!");
            }

            var parcel = Parcel as ICashOnDeliveryable;

            parcel.CODAmount = amount;
            IsCODEnabled = true;
        }

        public List<decimal> GetAllPrices()
        {
            var subtotal = CalculateTotalPrice();
            var total = subtotal - GetDiscount(subtotal);

            return new List<decimal> { GetBaseRate(), GetWeightCharge(), GetFragileCharge(),
                GetInsuranceCharge(), GetCODCharge(), GetDiscount(subtotal), total };
        }

        public decimal CalculateTotalPrice()
        {
            return GetBaseRate() + GetWeightCharge() + GetFragileCharge() + GetInsuranceCharge();
        }

        private decimal GetBaseRate()
        {
            return Tier switch
            {
                ServiceTier.SameDay => 200,
                ServiceTier.NextDay => 100,
                ServiceTier.Economy => 60,
                _ => 0
            };
        }

        private decimal GetWeightCharge()
        {
            var weightKg = Parcel.WeightKg;

            if (weightKg <= 1)
                return 0;

            if (weightKg <= 5)
                return 30;

            if (weightKg <= 15)
                return 80;

            return 150;
        }

        private decimal GetFragileCharge()
        {
            if (Parcel is IFragileable)
            {
                var fragile = Parcel as IFragileable;

                return fragile.FragileSurCharge();
            }

            return 0;
        }

        private decimal GetInsuranceCharge()
        {
            if (IsInsuranceTaken)
            {
                var insurance = Parcel as IInsuranceable;
                return insurance.InsuranceRate() * Parcel.DeclaredValue;
            }

            return 0;
        }

        private decimal GetDiscount(decimal total)
        {
            return Customer.DiscountRate() * total;
        }

        private decimal GetCODCharge()
        {
            if (IsCODEnabled)
            {
                var cashOnDeliveryable = Parcel as ICashOnDeliveryable;
                return cashOnDeliveryable.CODServiceRate() * cashOnDeliveryable.CODAmount;
            }

            return 0;
        }

        public void AdvanceStatus(ParcelStatus newStatus)
        {
            if (!IsValidTransition(Status, newStatus))
            {
                throw new Exception(
                    $"Invalid status transition: {Status} -> {newStatus}");
            }

            Status = newStatus;

            if (newStatus == ParcelStatus.Delivered)
            {
                DeliveredAt = DateTime.UtcNow;
            }
        }

        private bool IsValidTransition(
            ParcelStatus current,
            ParcelStatus next)
        {
            if (current == ParcelStatus.Delivered ||
                current == ParcelStatus.Returned)
            {
                return false;
            }

            return current switch
            {
                ParcelStatus.Booked =>
                    next == ParcelStatus.PickedUp ||
                    next == ParcelStatus.Cancelled,

                ParcelStatus.PickedUp =>
                    next == ParcelStatus.InTransit,

                ParcelStatus.InTransit =>
                    next == ParcelStatus.OutForDelivery,

                ParcelStatus.OutForDelivery =>
                    next == ParcelStatus.Delivered ||
                    next == ParcelStatus.Failed,

                ParcelStatus.Failed =>
                    next == ParcelStatus.Returned,

                _ => false
            };
        }
    }
}
