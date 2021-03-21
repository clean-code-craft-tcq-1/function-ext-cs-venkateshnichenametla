using System.Resources;
namespace BatteryManagementSystem
{
    public class GermanResourceMananger : IResourceManager
    {
        public ResourceManager GetResourceManager()
        {
            return new ResourceManager("BatteryManagementSystem.ResourceDutch", typeof(ResourceDutch).Assembly);
        }
    }
}
