using System;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
namespace BatteryManagementSystem {
    public class BatteryChargingFactors
    {
        private static string language = "English";
        public static string Language{
            get { return language; }
            set { language = value; }
        }
        private static CultureInfo cultureInfo;
        private static ResourceManager resourceManager = null;
        public static bool IsBatteryConditionOk(BatteryManager batteryManager) {
            if (IsTemperatureValid(batteryManager) && IsStateOfChargeValid(batteryManager) && IsChargeRateValid(batteryManager))
            {
                return true;
            }
            return false;
        }
        public static bool IsTemperatureValid(BatteryManager batteryManager)
        {
            bool isValid = new TemperatureValidator().IsValid(batteryManager);
            DisplayMessageFromResourceFile(nameof(ResourceEnglish.TemperatueOutOfRange));
            return isValid;
        }
        public static bool IsStateOfChargeValid(BatteryManager batteryManager)
        {
            bool isValid = new StateOfChargeValidator().IsValid(batteryManager);
            DisplayMessageFromResourceFile(nameof(ResourceEnglish.StateOfChargeOutOfRange));
            return isValid;
        }
        public static bool IsChargeRateValid(BatteryManager batteryManager)
        {
            bool isValid = new ChargeRateValidator().IsValid(batteryManager);
            DisplayMessageFromResourceFile(nameof(ResourceEnglish.ChargeRateOutOfRange));
            return isValid;
        }
        private static void DisplayMessageFromResourceFile(string resourceKey) {
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
            Console.WriteLine(resourceManager.GetString(resourceKey, cultureInfo));
        }
        public static BreachLevel GetTemperatureBreachLevel(BatteryManager batteryManager) {
            TemperatureValidator temperatureValidator = new TemperatureValidator();
            BreachLevel breachLevel = temperatureValidator.GetBreachLevel(batteryManager);
            if (breachLevel == BreachLevel.Low)
                DisplayMessageFromResourceFile(nameof(ResourceEnglish.TemperatureBreachLow));
            else if (breachLevel == BreachLevel.High)
                DisplayMessageFromResourceFile(nameof(ResourceEnglish.TemperatureBreachHigh));
            return breachLevel;
        }
        public static BreachLevel GetChargeRateBreachLevel(BatteryManager batteryManager) {
            ChargeRateValidator chargeRateValidator = new ChargeRateValidator();
            BreachLevel breachLevel = chargeRateValidator.GetBreachLevel(batteryManager);
            if (breachLevel == BreachLevel.Low)
                DisplayMessageFromResourceFile(nameof(ResourceEnglish.ChargeRateBreachLow));
            else if (breachLevel == BreachLevel.High)
                DisplayMessageFromResourceFile(nameof(ResourceEnglish.ChargeRateBreachHigh));
            return breachLevel;
        }
        public static BreachLevel GetStateOfChargeBreachLevel(BatteryManager batteryManager){
            StateOfChargeValidator stateOfChargeValidator = new StateOfChargeValidator();
            BreachLevel breachLevel = stateOfChargeValidator.GetBreachLevel(batteryManager);
            if (breachLevel == BreachLevel.Low)
                DisplayMessageFromResourceFile(nameof(ResourceEnglish.StateOfChargeBreachLow));
            else if (breachLevel == BreachLevel.High)
                DisplayMessageFromResourceFile(nameof(ResourceEnglish.StateOfChargeBreachHigh));
            return breachLevel;
        }
        public static ToleranceLevel GetTemperatureToleranceLevel(BatteryManager batteryManager){
            TemperatureValidator temperatureValidator = new TemperatureValidator();
            ToleranceLevel toleranceLevel = temperatureValidator.GetToleranceLevel(batteryManager);
            if (toleranceLevel == ToleranceLevel.ApproachingDischarnge)
                DisplayMessageFromResourceFile(nameof(ResourceEnglish.TemperatureToleranceLow));
            else if (toleranceLevel == ToleranceLevel.ApproachingPeak)
                DisplayMessageFromResourceFile(nameof(ResourceEnglish.TemperatureToleranceHigh));
            return toleranceLevel;
        }
        public static ToleranceLevel GetChargeRateToleranceLevel(BatteryManager batteryManager) {
            ChargeRateValidator chargeRateValidator = new ChargeRateValidator();
            ToleranceLevel toleranceLevel = chargeRateValidator.GetToleranceLevel(batteryManager);
            if (toleranceLevel == ToleranceLevel.ApproachingDischarnge)
                DisplayMessageFromResourceFile(nameof(ResourceEnglish.ChargeRateToleranceLow));
            else if (toleranceLevel == ToleranceLevel.ApproachingPeak) 
                DisplayMessageFromResourceFile(nameof(ResourceEnglish.ChargeRateToleranceHigh));
            return toleranceLevel;
        }
        public static ToleranceLevel GetStateOfChargeToleranceLevel(BatteryManager batteryManager) {
            StateOfChargeValidator stateOfChargeValidator = new StateOfChargeValidator();
            ToleranceLevel toleranceLevel = stateOfChargeValidator.GetToleranceLevel(batteryManager);
            if (toleranceLevel == ToleranceLevel.ApproachingDischarnge)
                DisplayMessageFromResourceFile(nameof(ResourceEnglish.StateOfChargeToleranceLow));
            else if (toleranceLevel == ToleranceLevel.ApproachingPeak)
                DisplayMessageFromResourceFile(nameof(ResourceEnglish.StateOfChargeToleranceHigh));
            return toleranceLevel;
        }
        static int Main() {
            Debug.Assert(IsBatteryConditionOk(new BatteryManager(25, 65, 0.7f)));
            Debug.Assert(!IsBatteryConditionOk(new BatteryManager(50, 65, 0.7f)));
            Debug.Assert(IsBatteryConditionOk(new BatteryManager(25, 65, 0.7f)));
            Debug.Assert(!IsBatteryConditionOk(new BatteryManager(25, 105, 0.7f)));
            Debug.Assert(IsBatteryConditionOk(new BatteryManager(25, 65, 0.7f)));
            Debug.Assert(!IsBatteryConditionOk(new BatteryManager(25, 65, 0.9f)));
            Debug.Assert(GetTemperatureBreachLevel(new BatteryManager(-5, 65, 0.7f)) == BreachLevel.Low);
            Debug.Assert(GetTemperatureBreachLevel(new BatteryManager(15, 65, 0.7f)) == BreachLevel.Normal);
            Debug.Assert(GetTemperatureBreachLevel(new BatteryManager(80, 65, 0.7f)) == BreachLevel.High);
            Debug.Assert(GetStateOfChargeBreachLevel(new BatteryManager(15, 10, 0.7f)) == BreachLevel.Low);
            Debug.Assert(GetStateOfChargeBreachLevel(new BatteryManager(15, 50, 0.7f)) == BreachLevel.Normal);
            Debug.Assert(GetStateOfChargeBreachLevel(new BatteryManager(15, 100, 0.7f)) == BreachLevel.High);
            Debug.Assert(GetChargeRateBreachLevel(new BatteryManager(15, 65, 0.2f)) == BreachLevel.Low);
            Debug.Assert(GetChargeRateBreachLevel(new BatteryManager(15, 65, 0.7f)) == BreachLevel.Normal);
            Debug.Assert(GetChargeRateBreachLevel(new BatteryManager(15, 65, 0.9f)) == BreachLevel.High);
            Debug.Assert(GetTemperatureToleranceLevel(new BatteryManager(2, 65, 0.7f)) == ToleranceLevel.ApproachingDischarnge);
            Debug.Assert(GetTemperatureToleranceLevel(new BatteryManager(15, 65, 0.7f)) == ToleranceLevel.Normal);
            Debug.Assert(GetTemperatureToleranceLevel(new BatteryManager(43, 65, 0.7f)) == ToleranceLevel.ApproachingPeak);
            Debug.Assert(GetStateOfChargeToleranceLevel(new BatteryManager(15, 21, 0.7f)) == ToleranceLevel.ApproachingDischarnge);
            Debug.Assert(GetStateOfChargeToleranceLevel(new BatteryManager(15, 50, 0.7f)) == ToleranceLevel.Normal);
            Debug.Assert(GetStateOfChargeToleranceLevel(new BatteryManager(15, 78, 0.7f)) == ToleranceLevel.ApproachingPeak);
            Debug.Assert(GetChargeRateToleranceLevel(new BatteryManager(15, 65, 0.33f)) == ToleranceLevel.ApproachingDischarnge);
            Debug.Assert(GetChargeRateToleranceLevel(new BatteryManager(15, 65, 0.7f)) == ToleranceLevel.Normal);
            Debug.Assert(GetChargeRateToleranceLevel(new BatteryManager(15, 65, 0.77f)) == ToleranceLevel.ApproachingPeak);
            Console.WriteLine("All ok");
            return 0;
        }
    }
}