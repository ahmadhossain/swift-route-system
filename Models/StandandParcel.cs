using SwiftRoute_Courier___OOP_Assesment.Interfaces;

namespace SwiftRoute_Courier___OOP_Assesment.Models
{
    class StandandParcel : Parcel, IInsurance, ICashOnDelivery 
    {
        private double _weightKg;
        private double _maxWeightLimit = 30;
        private double _minWeightLimit = 0.1;
        public decimal CODAmount { get; set; }
        public override double WeightKg
        {
            get { return _weightKg; }
            protected set
            {
                if (value < _minWeightLimit || value > _maxWeightLimit )
                {
                    throw new Exception("Weight can not exceed 30kg or below 0.1kg!");
                }

                _weightKg = value;
            }
        }

        public bool IsInsuranceMandatory() => false;

        public StandandParcel(double weightKg) : base("Standand")
        {
            WeightKg = weightKg;
        }
    }
}
