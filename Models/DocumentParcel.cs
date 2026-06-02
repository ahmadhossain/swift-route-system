using System.Reflection.Metadata.Ecma335;

namespace SwiftRoute_Courier___OOP_Assesment.Models
{
    class DocumentParcel : Parcel
    {
        private double _weightKg;
        private double _maxWeightLimit = 0.5;
        public override double WeightKg
        {
            get { return _weightKg; }
            protected set
            {
                if (value > _maxWeightLimit)
                {
                    throw new Exception("Weight can not exceed 0.5kg!");
                }

                _weightKg = value;
            }
        }

        public DocumentParcel(double weightKg) : base("Document")
        {
            WeightKg = weightKg;
        }
    }
}
