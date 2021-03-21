using System;
using System.Collections.Generic;

namespace BatteryManagementSystem
{
    public class ConsoleReporter : IReporter
    {
        public void Report(List<string> messages)
        {
            foreach (string message in messages)
                Console.WriteLine(message);
        }
    }
}
