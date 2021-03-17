using System.IO;
namespace BatteryManagementSystem
{
    class TextFileReporter : IReporter
    {
        public void Report(string message)
        {
            File.WriteAllText(Path.GetTempPath(), message);
        }
    }
}
