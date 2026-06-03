using SwiftRoute_Courier___OOP_Assesment.Enums;

namespace SwiftRoute_Courier___OOP_Assesment.Interfaces
{
    interface ICourier
    {
        double MaxWeightLimit();
        double MinWeightLimit();
        bool IsTierEligible(ServiceTier serviceTier);
    }
}
