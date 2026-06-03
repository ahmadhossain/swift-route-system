using SwiftRoute_Courier___OOP_Assesment.Enums;
using SwiftRoute_Courier___OOP_Assesment.Interfaces;

namespace SwiftRoute_Courier___OOP_Assesment.Models
{
    abstract class Courier : ICourier
    {
        public abstract double MaxWeightLimit();
        public abstract double MinWeightLimit();
        public abstract bool IsTierEligible(ServiceTier serviceTier);
    }
}
