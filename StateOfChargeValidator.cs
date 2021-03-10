namespace BatteryManagementSystem {
    public class StateOfChargeValidator : IValidator<BatteryManager> {
        private static readonly float stateOfChargeMinimum = 20;
        private static readonly float stateOfChargeMaximum = 80;
        static readonly float lowToleranceValue = (float)(stateOfChargeMinimum + (stateOfChargeMaximum * 0.05));
        static readonly float highToleranceValue = (float)(stateOfChargeMaximum - (stateOfChargeMaximum * 0.05));
        //Pure function
        public bool IsValid(BatteryManager batteryManager) {
            return !(batteryManager.StateOfCharge < stateOfChargeMinimum || batteryManager.StateOfCharge > stateOfChargeMaximum);
        }
        //Pure function
        public BreachLevel GetBreachLevel(BatteryManager batteryManager) {
            if (batteryManager.StateOfCharge < stateOfChargeMinimum)
                return BreachLevel.Low;
            else if (batteryManager.StateOfCharge > stateOfChargeMaximum)
                return BreachLevel.High;
            return BreachLevel.Normal;
        }
        //Pure function
        public ToleranceLevel GetToleranceLevel(BatteryManager batteryManager) {
            if (batteryManager.StateOfCharge < lowToleranceValue)
                return ToleranceLevel.ApproachingDischarnge;
            else if (batteryManager.StateOfCharge > highToleranceValue)
                return ToleranceLevel.ApproachingPeak;
            return ToleranceLevel.Normal;
        }
    }
}
