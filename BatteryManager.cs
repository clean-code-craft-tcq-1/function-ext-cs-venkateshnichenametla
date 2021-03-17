using System.Globalization;
using System.Resources;
namespace BatteryManagementSystem 
{
    public class BatteryManager 
    {
        IReporter reporter;

        public BatteryManager(IReporter reporterDependency)
        {
            reporter = reporterDependency;
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
            return IsTemperatureValid(temperature) && IsStateOfChargeValid(stateOfCharge) && IsChargeRateValid(chargeRate);
        }
        public bool IsTemperatureValid(float temperature)
        {
            bool isValid = new TemperatureValidator().IsValid(temperature);
            DisplayMessageFromResourceFile(nameof(ResourceEnglish.TemperatueOutOfRange));
            return isValid;
        }
        public bool IsStateOfChargeValid(float stateOfCharge)
        {
            bool isValid = new StateOfChargeValidator().IsValid(stateOfCharge);
            DisplayMessageFromResourceFile(nameof(ResourceEnglish.StateOfChargeOutOfRange));
            return isValid;
        }
        public bool IsChargeRateValid(float chargeRate)
        {
            bool isValid = new ChargeRateValidator().IsValid(chargeRate);
            DisplayMessageFromResourceFile(nameof(ResourceEnglish.ChargeRateOutOfRange));
            return isValid;
        }

        public bool IsBatteryBreached(float temperature, float stateOfCharge, float chargeRate)
        {
            return IsTemperatureLevelBreached(temperature) || IsStateOfChargeLevelBreached(stateOfCharge) || IsChargeRateLevelBreached(chargeRate);
        }
        private void DisplayMessageFromResourceFile(string resourceKey)
        {
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
            reporter.Report(resourceManager.GetString(resourceKey, cultureInfo));
        }
        public bool IsTemperatureLevelBreached(float temperature)
        {
            TemperatureValidator temperatureValidator = new TemperatureValidator();
            BreachLevel breachLevel = temperatureValidator.GetBreachLevel(temperature);
            if (breachLevel != BreachLevel.Normal)
            {
                DisplayMessageFromResourceFile(nameof(ResourceEnglish.TemperatureBreachHigh));
                return true;
            }
            return false;
        }
        public bool IsChargeRateLevelBreached(float chargeRate)
        {
            ChargeRateValidator chargeRateValidator = new ChargeRateValidator();
            BreachLevel breachLevel = chargeRateValidator.GetBreachLevel(chargeRate);
            if (breachLevel != BreachLevel.Normal)
            {
                DisplayMessageFromResourceFile(nameof(ResourceEnglish.ChargeRateBreachLow));
                return true;
            }
            return false;
        }
        public bool IsStateOfChargeLevelBreached(float stateOfCharge)
        {
            StateOfChargeValidator stateOfChargeValidator = new StateOfChargeValidator();
            BreachLevel breachLevel = stateOfChargeValidator.GetBreachLevel(stateOfCharge);
            if (breachLevel != BreachLevel.Normal)
                return true;
            return false;
        }

        public bool IsToleranceLevelVoilated(float temperature, float stateOfCharge, float chargeRate)
        {
            return IsTemperatureToleranceVoilated(temperature) || IsStateOfChargeToleranceVoilated(stateOfCharge) || IsChargeRateToleranceVoilated(chargeRate);
        }
        public bool IsTemperatureToleranceVoilated(float temperature)
        {
            TemperatureValidator temperatureValidator = new TemperatureValidator();
            ToleranceLevel toleranceLevel = temperatureValidator.GetToleranceLevel(temperature);
            if (toleranceLevel != ToleranceLevel.Normal)
            {
                DisplayMessageFromResourceFile(nameof(ResourceEnglish.TemperatureToleranceHigh));
                return true;
            }
            return false;
        }
        public bool IsChargeRateToleranceVoilated(float chargeRate)
        {
            ChargeRateValidator chargeRateValidator = new ChargeRateValidator();
            ToleranceLevel toleranceLevel = chargeRateValidator.GetToleranceLevel(chargeRate);
            if (toleranceLevel != ToleranceLevel.Normal)
            {
                DisplayMessageFromResourceFile(nameof(ResourceEnglish.ChargeRateToleranceHigh));
                return true;
            }
            return false;
        }
        public bool IsStateOfChargeToleranceVoilated(float stateOfCharge)
        {
            StateOfChargeValidator stateOfChargeValidator = new StateOfChargeValidator();
            ToleranceLevel toleranceLevel = stateOfChargeValidator.GetToleranceLevel(stateOfCharge);
            if (toleranceLevel != ToleranceLevel.Normal)
            {
                DisplayMessageFromResourceFile(nameof(ResourceEnglish.StateOfChargeToleranceLow));
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
