using System.Collections.Generic;
namespace BatteryManagementSystem
{
    public interface IReporter
    {
        void Report(List<string> messages);
    }
}
