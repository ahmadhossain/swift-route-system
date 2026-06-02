using SwiftRoute_Courier___OOP_Assesment.Interfaces;

namespace SwiftRoute_Courier___OOP_Assesment.Models
{
    class FragileParcel : Parcel, IFragileable, IInsuranceable, ICashOnDeliveryable
    {
        private double _weightKg;
        private double _maxWeightLimit = 15;
        public decimal CODAmount { get; set; }
        public override double WeightKg
        {
            get { return _weightKg; }
            protected set
            {
                if (value > _maxWeightLimit)
                {
                    throw new Exception("Weight can not exceed 15kg!");
                }

                _weightKg = value;
            }
        }

        public bool IsInsuranceMandatory() => true;

        public FragileParcel(double weight) : base("Fragile")
        {
            WeightKg = weight;
        }
    }
}
