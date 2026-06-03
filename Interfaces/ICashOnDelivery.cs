namespace SwiftRoute_Courier___OOP_Assesment.Interfaces
{
    interface ICashOnDelivery
    {
        decimal CODAmount { get; set; }
        decimal GetCODServiceRate() => 0.01m;
    }
}
