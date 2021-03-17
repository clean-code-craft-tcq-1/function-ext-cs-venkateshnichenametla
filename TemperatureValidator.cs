namespace BatteryManagementSystem {
    public class TemperatureValidator
    {
        private static readonly float temperatureMinimum = 0;
        private static readonly float temperatureMaximum = 45;
        static readonly float lowToleranceValue = (float)(temperatureMinimum + (temperatureMaximum * 0.05));
        static readonly float highToleranceValue = (float)(temperatureMaximum - (temperatureMaximum * 0.05));
        //Pure function
        public bool IsValid(float temperature) 
        {
            return ((temperature > temperatureMinimum) && (temperature < temperatureMaximum));
        }
        //Pure function
        public BreachLevel GetBreachLevel(float temperature) 
        {
            if (temperature < temperatureMinimum)
                return BreachLevel.Low;
            else if (temperature > temperatureMaximum)
                return BreachLevel.High;
            return BreachLevel.Normal;
        }
        //Pure function
        public ToleranceLevel GetToleranceLevel(float temperature) 
        {
            if (IsTemperatureAtLowTolerance(temperature))
                return ToleranceLevel.ApproachingDischarnge;
            else if (IsTemperatureAtHighTolerance(temperature))
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
