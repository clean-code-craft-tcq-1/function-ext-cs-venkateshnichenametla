namespace BatteryManagementSystem {
    public class TemperatureValidator : IValidator<BatteryManager> {
        private static readonly float temperatureMinimum = 0;
        private static readonly float temperatureMaximum = 45;
        static readonly float lowToleranceValue = (float)(temperatureMinimum + (temperatureMaximum * 0.05));
        static readonly float highToleranceValue = (float)(temperatureMaximum - (temperatureMaximum * 0.05));
        //Pure function
        public bool IsValid(BatteryManager batteryManager) {
            return !(batteryManager.Temperature < temperatureMinimum || batteryManager.Temperature > temperatureMaximum);
        }
        //Pure function
        public BreachLevel GetBreachLevel(BatteryManager batteryManager) {
            if (batteryManager.Temperature < temperatureMinimum)
                return BreachLevel.Low;
            else if (batteryManager.Temperature > temperatureMaximum)
                return BreachLevel.High;
            return BreachLevel.Normal;
        }
        //Pure function
        public ToleranceLevel GetToleranceLevel(BatteryManager batteryManager) {
            if (batteryManager.Temperature > temperatureMinimum && batteryManager.Temperature < lowToleranceValue)
                return ToleranceLevel.ApproachingDischarnge;
            else if (batteryManager.Temperature < temperatureMaximum && batteryManager.Temperature > highToleranceValue)
                return ToleranceLevel.ApproachingPeak;
            return ToleranceLevel.Normal;
        }
    }
}
