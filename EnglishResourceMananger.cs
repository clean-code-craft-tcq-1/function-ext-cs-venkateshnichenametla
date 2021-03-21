using System.Resources;
namespace BatteryManagementSystem
{
    public class EnglishResourceMananger : IResourceManager
    {
        public ResourceManager GetResourceManager()
        {
            return new ResourceManager("BatteryManagementSystem.ResourceEnglish", typeof(ResourceEnglish).Assembly);
        }
    }
}
