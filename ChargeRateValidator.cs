namespace BatteryManagementSystem 
{
    public class ChargeRateValidator 
    {
        static readonly float chargeRateMaximum = 0.8f;
        static readonly float chargeRateMinimum = 0.3f;
        static readonly float lowToleranceValue = (float)(chargeRateMinimum + (chargeRateMaximum * 0.05));
        static readonly float highToleranceValue = (float)(chargeRateMaximum - (chargeRateMaximum * 0.05));
        //Pure function
        public bool IsValid(float chargeRate) 
        {
            return ((chargeRate > chargeRateMinimum) && (chargeRate < chargeRateMaximum));
        }

        //Pure function
        public BreachLevel GetBreachLevel(float chargeRate) 
        {
            if (chargeRate < chargeRateMinimum)
                return BreachLevel.Low;
            else if (chargeRate > chargeRateMaximum)
                return BreachLevel.High;
            return BreachLevel.Normal;
        }

        //Pure function
        public ToleranceLevel GetToleranceLevel(float chargeRate) 
        {
            if (IsChargeRateAtLowTolerance(chargeRate))
                return ToleranceLevel.ApproachingDischarnge;
            else if (IsChargeRateAtHighTolerance(chargeRate))
                return ToleranceLevel.ApproachingPeak;
            return ToleranceLevel.Normal;
        }
        private bool IsChargeRateAtLowTolerance(float currentStateOfCharnge) 
        {
            return currentStateOfCharnge > chargeRateMinimum && currentStateOfCharnge < lowToleranceValue;
        }
        private bool IsChargeRateAtHighTolerance(float currentStateOfCharnge) 
        {
            return currentStateOfCharnge < chargeRateMaximum && currentStateOfCharnge > highToleranceValue;
        }
    }
}
