using System.IO;
using System.Collections.Generic;
namespace BatteryManagementSystem
{
    class TextFileReporter : IReporter
    {
        public void Report(List<string> messages)
        {
            string filePath = Path.Combine(Path.GetTempPath(), "BatteryReporter.txt");
            if (File.Exists(filePath))
                File.Delete(filePath);
            File.AppendAllLines(filePath, messages);
        }
    }
}
