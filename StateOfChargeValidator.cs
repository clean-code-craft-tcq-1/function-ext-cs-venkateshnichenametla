namespace BatteryManagementSystem {
    public class StateOfChargeValidator 
    {
        private static readonly float stateOfChargeMinimum = 20;
        private static readonly float stateOfChargeMaximum = 80;
        static readonly float lowToleranceValue = (float)(stateOfChargeMinimum + (stateOfChargeMaximum * 0.05));
        static readonly float highToleranceValue = (float)(stateOfChargeMaximum - (stateOfChargeMaximum * 0.05));
        //Pure function
        public bool IsValid(float stateOfCharge) 
        {
            return ((stateOfCharge > stateOfChargeMinimum) && (stateOfCharge < stateOfChargeMaximum));
        }
        //Pure function
        public BreachLevel GetBreachLevel(float stateOfCharge) {
            if (stateOfCharge < stateOfChargeMinimum)
                return BreachLevel.Low;
            else if (stateOfCharge > stateOfChargeMaximum)
                return BreachLevel.High;
            return BreachLevel.Normal;
        }
        //Pure function
        public ToleranceLevel GetToleranceLevel(float stateOfCharge) 
        {
            if (IsStateOfChargeAtLowTolerance(stateOfCharge))
                return ToleranceLevel.ApproachingDischarnge;
            else if (IsStateOfChargeAtHighTolerance(stateOfCharge))
                return ToleranceLevel.ApproachingPeak;
            return ToleranceLevel.Normal;
        }
        private bool IsStateOfChargeAtLowTolerance(float currentStateOfCharnge) {
            return currentStateOfCharnge > stateOfChargeMinimum && currentStateOfCharnge < lowToleranceValue;
        }
        private bool IsStateOfChargeAtHighTolerance(float currentStateOfCharnge) {
            return currentStateOfCharnge < stateOfChargeMaximum && currentStateOfCharnge > highToleranceValue;
        }
    }
}
