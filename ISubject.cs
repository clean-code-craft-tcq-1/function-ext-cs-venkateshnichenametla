namespace BatteryManagementSystem
{
    interface ISubject
    {
        void Register(IObserver observer);
        void Notify();
    }
}
