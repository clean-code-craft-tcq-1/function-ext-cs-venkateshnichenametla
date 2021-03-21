using System.Resources;
namespace BatteryManagementSystem
{
    public static class ResourceManagerHelper
    {
        private static ResourceManager _resourceManager;
        public static void SetResourceManager(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public static ResourceManager GetResourceManager()
        {
            return _resourceManager;
        }
    }
}
