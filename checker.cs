using System;
using System.Diagnostics;
using System.Resources;

namespace BatteryManagementSystem 
{
    public class Checker
    {
        static int Main()
        {
            ResourceManagerHelper.SetResourceManager(new GermanResourceMananger().GetResourceManager());
            BatteryManager batteryManager = new BatteryManager();
            Accumulator accumulator = new Accumulator();
            batteryManager.Register(accumulator);
            Debug.Assert(batteryManager.IsBatteryConditionOk(25, 65, 0.7f));
            Debug.Assert(!batteryManager.IsBatteryConditionOk(50, 65, 0.7f));
            Debug.Assert(!batteryManager.IsBatteryConditionOk(25, 105, 0.7f));
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
            Reporter reporter = new Reporter(new ConsoleReporter());
            reporter.ReportMessages(accumulator.GetAccumulatedReport());
            Reporter reporterTextFile = new Reporter(new TextFileReporter());
            reporterTextFile.ReportMessages(accumulator.GetAccumulatedReport());
            Console.WriteLine("All ok");
            Console.ReadLine();
            return 0;
        }
    }
}