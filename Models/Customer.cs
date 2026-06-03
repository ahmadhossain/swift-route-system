namespace SwiftRoute_Courier___OOP_Assesment.Models
{
    abstract class Customer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
    }
}
