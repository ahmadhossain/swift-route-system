using SwiftRoute_Courier___OOP_Assesment.Enums;

namespace SwiftRoute_Courier___OOP_Assesment.Models
{
    class TruckCourier : Courier
    {
        public override double MaxWeightLimit() => double.MaxValue;
        public override double MinWeightLimit() => 20;
        public override bool IsTierEligible(ServiceTier serviceTier)
        {
            return serviceTier == ServiceTier.Economy;
        }
    }
}
