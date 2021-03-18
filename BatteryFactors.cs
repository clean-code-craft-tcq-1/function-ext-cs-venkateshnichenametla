namespace BatteryManagementSystem
{
    public class BatteryFactors
    {
        public const string Temperature = "Temperature";
        public const float TemperatureMinimum = 0;
        public const float TemperatureMaximum = 45;
        public const float TemperatureLowTolerance = (float)(TemperatureMinimum + (TemperatureMaximum * 0.05));
        public const float TemperatureHighTolerance = (float)(TemperatureMaximum - (TemperatureMaximum * 0.05));
        public const string StateOfCharge = "State of Charge";
        public const float StateOfChargeMinimum = 20;
        public const float StateOfChargeMaximum = 80;
        public const float StateOfChargeLowTolerance = (float)(StateOfChargeMinimum + (StateOfChargeMaximum * 0.05));
        public const float StateOfChargeHighTolerance = (float)(StateOfChargeMaximum - (StateOfChargeMaximum * 0.05));
        public const string ChargeRate = "Charge Rate";
        public const float ChargeRateMinimum = 0.3f;
        public const float ChargeRateMaximum = 0.8f;
        public const float ChargeRateLowTolerance = (float)(ChargeRateMinimum + (ChargeRateMaximum * 0.05));
        public const float ChargeRateHighTolerance = (float)(ChargeRateMaximum - (ChargeRateMaximum * 0.05));
    }
}
