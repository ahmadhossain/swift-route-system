namespace SwiftRoute_Courier___OOP_Assesment.Interfaces
{
    interface ICashOnDeliveryable
    {
        decimal CODAmount { get; set; }

        decimal CODServiceRate() => 0.01m;
    }
}
