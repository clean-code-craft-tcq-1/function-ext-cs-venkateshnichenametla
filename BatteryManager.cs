using System.Collections.Generic;
namespace BatteryManagementSystem
{
    public class BatteryManager : ISubject
    {
        List<IObserver> observers;
        public BatteryManager()
        {
            observers = new List<IObserver>();
            Messages = new List<string>();
        }
        public List<string> Messages { get; set; }
        public bool IsBatteryConditionOk(float temperature, float stateOfCharge, float chargeRate)
        {
            return IsBatteryFactorValid(BatteryFactors.Temperature, temperature, BatteryFactors.TemperatureMinimum, BatteryFactors.TemperatureMaximum)
                && IsBatteryFactorValid(BatteryFactors.StateOfCharge, stateOfCharge, BatteryFactors.StateOfChargeMinimum, BatteryFactors.StateOfChargeMaximum)
                && IsBatteryFactorValid(BatteryFactors.ChargeRate, chargeRate, BatteryFactors.ChargeRateMinimum, BatteryFactors.ChargeRateMaximum);
        }
        public bool IsBatteryBreached(float temperature, float stateOfCharge, float chargeRate)
        {
            return IsBatteryLowBreached(temperature, stateOfCharge, chargeRate)
                || IsBatteryHighBreached(temperature, stateOfCharge, chargeRate);
        }
        private bool IsBatteryLowBreached(float temperature, float stateOfCharge, float chargeRate)
        {
            return IsLowBreach(BatteryFactors.Temperature, temperature, BatteryFactors.TemperatureMinimum) ||
                   IsLowBreach(BatteryFactors.StateOfCharge, stateOfCharge, BatteryFactors.StateOfChargeMinimum) ||
                   IsLowBreach(BatteryFactors.ChargeRate, chargeRate, BatteryFactors.ChargeRateMinimum);
        }
        private bool IsBatteryHighBreached(float temperature, float stateOfCharge, float chargeRate)
        {
            return IsHighBreach(BatteryFactors.Temperature, temperature, BatteryFactors.TemperatureMaximum) ||
                   IsHighBreach(BatteryFactors.StateOfCharge, stateOfCharge, BatteryFactors.StateOfChargeMaximum) ||
                   IsHighBreach(BatteryFactors.ChargeRate, chargeRate, BatteryFactors.ChargeRateMaximum);
        }
        public bool IsBatteryToleranceVoildated(float temperature, float stateOfCharge, float chargeRate)
        {
            return IsBatteryApproachingDischarge(temperature, stateOfCharge, chargeRate) || IsBatteryApproachingPeak(temperature, stateOfCharge, chargeRate);
        }
        private bool IsBatteryApproachingDischarge(float temperature, float stateOfCharge, float chargeRate)
        {
            return IsApproachingDischarge(BatteryFactors.Temperature, temperature, BatteryFactors.TemperatureMinimum, BatteryFactors.TemperatureLowTolerance) ||
                   IsApproachingDischarge(BatteryFactors.StateOfCharge, stateOfCharge, BatteryFactors.StateOfChargeMinimum, BatteryFactors.StateOfChargeLowTolerance) ||
                   IsApproachingDischarge(BatteryFactors.ChargeRate, chargeRate, BatteryFactors.ChargeRateMinimum, BatteryFactors.ChargeRateLowTolerance);
        }
        private bool IsBatteryApproachingPeak(float temperature, float stateOfCharge, float chargeRate)
        {
            return IsApproachingPeak(BatteryFactors.Temperature, temperature, BatteryFactors.TemperatureMaximum, BatteryFactors.TemperatureHighTolerance) ||
                   IsApproachingPeak(BatteryFactors.StateOfCharge, stateOfCharge, BatteryFactors.StateOfChargeMaximum, BatteryFactors.StateOfChargeHighTolerance) ||
                   IsApproachingPeak(BatteryFactors.ChargeRate, chargeRate, BatteryFactors.ChargeRateMaximum, BatteryFactors.ChargeRateHighTolerance);
        }
        public bool IsBatteryFactorValid(string factorType, float currentValue, float mininumValue, float maximumValue)
        {
            if ((currentValue > mininumValue) && (currentValue < maximumValue))
                return true;
            string message = GetMessageFromResourceKey(nameof(ResourceEnglish.IsNotValid));
            LogMessage(factorType + " " + message);
            return false;
        }
        public bool IsLowBreach(string factorType, float currentValue, float lowBreachValue)
        {
            if (currentValue < lowBreachValue)
                return true;
            string message = GetMessageFromResourceKey(nameof(ResourceEnglish.IsLowBreach));
            LogMessage(factorType + " " + message);
            return false;
        }
        public bool IsHighBreach(string factorType, float currentValue, float highBreachValue)
        {
            if (currentValue > highBreachValue)
                return true;
            string message = GetMessageFromResourceKey(nameof(ResourceEnglish.IsHighBreach));
            LogMessage(factorType + " " + message);
            return false;
        }
        public bool IsApproachingDischarge(string factorType, float currentValue, float minimumValue, float lowToleranceValue)
        {
            if (currentValue > minimumValue && currentValue < lowToleranceValue)
                return true;
            string message = GetMessageFromResourceKey(nameof(ResourceEnglish.ApproachingDischarge));
            LogMessage(message + " - " + factorType);
            return false;
        }
        public bool IsApproachingPeak(string factorType, float currentValue, float maximumValue, float highToleranceValue)
        {
            if (currentValue < maximumValue && currentValue > highToleranceValue)
                return true;
            string message = GetMessageFromResourceKey(nameof(ResourceEnglish.ApproachingPeak));
            LogMessage(message + " - " + factorType);
            return false;
        }
        private string GetMessageFromResourceKey(string resourceKey)
        {
            return ResourceManagerHelper.GetResourceManager().GetString(resourceKey);
        }
        private void LogMessage(string message)
        {
            if (!Messages.Contains(message))
                Messages.Add(message);
            Notify();
        }
        public void Register(IObserver observer)
        {
            observers.Add(observer);
        }
        public void Notify()
        {
            observers.ForEach(observe => { observe.Update(Messages); });
        }
    }
    public enum BreachLevel
    {
        Low,
        Normal,
        High
    }
    public enum ToleranceLevel
    {
        ApproachingDischarnge,
        Normal,
        ApproachingPeak
    }
}