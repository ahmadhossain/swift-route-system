using SwiftRoute_Courier___OOP_Assesment.Enums;

namespace SwiftRoute_Courier___OOP_Assesment.Models
{
    class BikeCourier : Courier
    {
        public override double MaxWeightLimit() => 5;
        public override double MinWeightLimit() => 0;
        public override bool IsTierEligible(ServiceTier serviceTier)
        {
            return serviceTier == ServiceTier.SameDay || serviceTier == ServiceTier.NextDay;
        }
    }
}




