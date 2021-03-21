using System.Collections.Generic;
namespace BatteryManagementSystem
{
    public class Accumulator : IObserver
    {
        List<string> accumalatedReport = new List<string>();
        public Accumulator()
        {
            accumalatedReport.Clear();
        }
        public void Update(List<string> messages)
        {
            foreach (string message in messages)
            {
                if (!accumalatedReport.Contains(message))
                    accumalatedReport.Add(message);
            }
        }
        public List<string> GetAccumulatedReport()
        {
            return accumalatedReport;
        }
    }
}
