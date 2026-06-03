using SwiftRoute_Courier___OOP_Assesment.Interfaces;

namespace SwiftRoute_Courier___OOP_Assesment.Models
{
    abstract class Parcel
    {
        public PersonInfo Sender { get; set; }
        public PersonInfo Recipient { get; set; }
        public decimal DeclaredValue { get; set; }
        public abstract double WeightKg { get; protected set; }
        public string ParcelType { get; private set; }

        protected Parcel(string type)
        {
            ParcelType = type;
        }
    }
}
