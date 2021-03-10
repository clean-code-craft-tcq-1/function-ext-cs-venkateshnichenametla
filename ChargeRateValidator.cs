namespace BatteryManagementSystem {
    public class ChargeRateValidator : IValidator<BatteryManager> {
        static readonly float chargeRateMaximum = 0.8f;
        static readonly float chargeRateMinimum = 0.3f;
        static readonly float lowToleranceValue = (float)(chargeRateMinimum + (chargeRateMaximum * 0.05));
        static readonly float highToleranceValue = (float)(chargeRateMaximum - (chargeRateMaximum * 0.05));
        //Pure function
        public bool IsValid(BatteryManager batteryManager) {
            return !(batteryManager.ChargeRate > chargeRateMaximum);
        }
        //Pure function
        public BreachLevel GetBreachLevel(BatteryManager batteryManager) {
            if (batteryManager.ChargeRate < chargeRateMinimum)
                return BreachLevel.Low;
            else if (batteryManager.ChargeRate > chargeRateMaximum)
                return BreachLevel.High;
            return BreachLevel.Normal;
        }

        //Pure function
        public ToleranceLevel GetToleranceLevel(BatteryManager batteryManager) {
            if (batteryManager.ChargeRate < lowToleranceValue)
                return ToleranceLevel.ApproachingDischarnge;
            else if (batteryManager.ChargeRate > highToleranceValue)
                return ToleranceLevel.ApproachingPeak;
            return ToleranceLevel.Normal;
        }
    }
}
