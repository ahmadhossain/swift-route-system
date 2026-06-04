using SwiftRoute_Courier___OOP_Assesment.Interfaces;

namespace SwiftRoute_Courier___OOP_Assesment.Models
{
    class BusinessCustomer : Customer, IDiscount
    {
        public string CompanyName { get; set; }
        public decimal MonthlyCreditAmount { get; set; }

        public decimal GetDiscountRate() => 0.1m; 
    }
}
