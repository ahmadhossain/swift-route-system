namespace SwiftRoute_Courier___OOP_Assesment.Models
{
    class BusinessCustomer : Customer
    {
        public string CompanyName { get; set; }
        public decimal MonthlyCreditAmount { get; set; }
        public override decimal DiscountRate() => 0.10m;
    }
}
