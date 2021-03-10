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
            if (IsTemperatureAtLowTolerance(batteryManager.Temperature))
                return ToleranceLevel.ApproachingDischarnge;
            else if (IsTemperatureAtHighTolerance(batteryManager.Temperature))
                return ToleranceLevel.ApproachingPeak;
            return ToleranceLevel.Normal;
        }
        private bool IsTemperatureAtLowTolerance(float currentTemperature) {
            return currentTemperature > temperatureMinimum && currentTemperature < lowToleranceValue;
        }
        private bool IsTemperatureAtHighTolerance(float currentTemperature) {
            return currentTemperature < temperatureMaximum && currentTemperature > highToleranceValue;
        }
    }
}
