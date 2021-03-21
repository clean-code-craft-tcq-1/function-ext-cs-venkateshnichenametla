using System.Collections.Generic;
namespace BatteryManagementSystem
{
    public interface IObserver
    {
        void Update(List<string> report);
    }
}
