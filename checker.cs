using System;
using System.Diagnostics;
namespace BatteryManagementSystem 
{
    public class Checker
    {
        static int Main()
        {
            IReporter consoleReporter = new ConsoleReporter();
            BatteryManager batteryManager = new BatteryManager(consoleReporter);
            Debug.Assert(batteryManager.IsBatteryConditionOk(25, 65, 0.7f));
            Debug.Assert(!batteryManager.IsBatteryConditionOk(50, 65, 0.7f));
            Debug.Assert(batteryManager.IsBatteryConditionOk(25, 65, 0.7f));
            Debug.Assert(!batteryManager.IsBatteryConditionOk(25, 105, 0.7f));
            Debug.Assert(batteryManager.IsBatteryConditionOk(25, 65, 0.7f));
            Debug.Assert(!batteryManager.IsBatteryConditionOk(25, 65, 0.9f));
            Debug.Assert(batteryManager.IsBatteryBreached(-5, 65, 0.7f));
            Debug.Assert(!batteryManager.IsBatteryBreached(15, 65, 0.7f));
            Debug.Assert(batteryManager.IsBatteryBreached(15, 10, 0.7f));
            Debug.Assert(!batteryManager.IsBatteryBreached(15, 50, 0.7f));
            Debug.Assert(batteryManager.IsBatteryBreached(15, 65, 0.2f));
            Debug.Assert(!batteryManager.IsBatteryBreached(15, 65, 0.7f));
            Debug.Assert(batteryManager.IsBatteryToleranceVoildated(2, 65, 0.7f));
            Debug.Assert(!batteryManager.IsBatteryToleranceVoildated(15, 65, 0.7f));
            Debug.Assert(batteryManager.IsBatteryToleranceVoildated(15, 21, 0.7f));
            Debug.Assert(!batteryManager.IsBatteryToleranceVoildated(15, 50, 0.7f));
            Debug.Assert(batteryManager.IsBatteryToleranceVoildated(15, 65, 0.33f));
            Debug.Assert(!batteryManager.IsBatteryToleranceVoildated(15, 65, 0.7f));
            Console.WriteLine("All ok");
            return 0;
        }
    }
}