using SwiftRoute_Courier___OOP_Assesment.Enums;

namespace SwiftRoute_Courier___OOP_Assesment.Models
{
    abstract class Courier
    {
        public abstract double MaxWeightLimit();
        public abstract double MinWeightLimit();
        public abstract bool IsTierEligible(ServiceTier serviceTier);

        //public abstract ServiceTier EligibleTier() => ServiceTier.SameDay || ServiceTier.NextDay; 
    }
}
