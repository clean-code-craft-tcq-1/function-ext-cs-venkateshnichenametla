using System;
using System.Globalization;
using System.Resources;
namespace BatteryManagementSystem 
{
    public class BatteryManager 
    {
        IReporter reporter;

        public BatteryManager(IReporter reporterDependency)
        {
            if(reporterDependency == null)
            {
                throw new InvalidProgramException("Invalid program!!");
            }
            reporter = reporterDependency;
            if (Language.Equals("German"))
            {
                resourceManager = new ResourceManager("BatteryManagementSystem.ResourceDutch", typeof(ResourceDutch).Assembly);
                cultureInfo = CultureInfo.CreateSpecificCulture("de");
            }
            else
            {
                resourceManager = new ResourceManager("BatteryManagementSystem.ResourceEnglish", typeof(ResourceEnglish).Assembly);
                cultureInfo = CultureInfo.CreateSpecificCulture("en");
            }
        }

        private static string language = "English";
        public static string Language
        {
            get { return language; }
            set { language = value; }
        }
        private static CultureInfo cultureInfo;
        private static ResourceManager resourceManager = null;
        public bool IsBatteryConditionOk(float temperature, float stateOfCharge, float chargeRate)
        {
            return IsBatteryFactorValid(BatteryFactors.Temperature, temperature, BatteryFactors.TemperatureMinimum, BatteryFactors.TemperatureMaximum)
                && IsBatteryFactorValid(BatteryFactors.StateOfCharge, stateOfCharge, BatteryFactors.StateOfChargeMinimum, BatteryFactors.StateOfChargeMaximum)
                && IsBatteryFactorValid(BatteryFactors.ChargeRate, chargeRate, BatteryFactors.ChargeRateMinimum, BatteryFactors.ChargeRateMaximum);
        }

        public bool IsBatteryBreached(float temperature, float stateOfCharge, float chargeRate)
        {
            return IsBatteryLowBreached( temperature, stateOfCharge, chargeRate) 
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
            return IsApproachingPeak(BatteryFactors.Temperature,temperature, BatteryFactors.TemperatureMaximum, BatteryFactors.TemperatureHighTolerance) ||
                   IsApproachingPeak(BatteryFactors.StateOfCharge, stateOfCharge, BatteryFactors.StateOfChargeMaximum, BatteryFactors.StateOfChargeHighTolerance) ||
                   IsApproachingPeak(BatteryFactors.ChargeRate,chargeRate, BatteryFactors.ChargeRateMaximum, BatteryFactors.ChargeRateHighTolerance);
        }
        private void LogMessage(string message)
        {
            reporter.Report(message);
        }
        public bool IsBatteryFactorValid(string factorType,float currentValue, float mininumValue, float maximumValue)
        {
            if ((currentValue > mininumValue) && (currentValue < maximumValue))
            {
                LogMessage(factorType + "is not valid!");
                return true;
            }
            return false;
        }
        public bool IsLowBreach(string factorType, float currentValue,float lowBreachValue )
        {
            if (currentValue < lowBreachValue)
            {
                LogMessage(factorType + " is low breach");
                return true;
            }
            return false;
        }
        public bool IsHighBreach(string factorType, float currentValue, float highBreachValue)
        {
            if (currentValue > highBreachValue)
            {
                LogMessage(factorType + " is high breach");
                return true;
            }
            return false;
        }
        public bool IsApproachingDischarge(string factorType, float currentValue, float minimumValue, float lowToleranceValue)
        {
            if(currentValue > minimumValue && currentValue < lowToleranceValue)
            {
                LogMessage("Warning!!" + factorType + " Approaching discharge");
                return true;
            }
            return false;
        }
        public bool IsApproachingPeak(string factorType,float currentValue,float maximumValue , float highToleranceValue)
        {
            if(currentValue < maximumValue && currentValue > highToleranceValue)
            {
                LogMessage("Warning!!" + factorType + " Approaching peak");
                return true;
            }
            return false;
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