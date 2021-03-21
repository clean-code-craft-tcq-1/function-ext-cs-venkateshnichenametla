using System;
using System.Collections.Generic;
namespace BatteryManagementSystem
{
    public class Reporter
    {
        private readonly IReporter reporter;
        public Reporter(IReporter reporterDependency)
        {
            if (reporterDependency == null)
            {
                throw new InvalidProgramException("Invalid program!!");
            }
            reporter = reporterDependency;
        }

        public void ReportMessages(List<string> messages)
        {
            reporter.Report(messages);
        }
    }
}
