namespace SwiftRoute_Courier___OOP_Assesment.Interfaces
{
    interface IInsurance
    {
        decimal GetInsuranceRate() => 0.02m;
        bool IsInsuranceMandatory();
    }
}
