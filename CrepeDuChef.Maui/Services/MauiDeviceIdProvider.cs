using CrepeDuChef.Application.Interfaces;

namespace CrepeDuChef.Maui.Services
{
    public class MauiDeviceIdProvider : IDeviceIdProvider
    {
        private const string Key = "DeviceId";
        public Guid DeviceId { get; }

        public MauiDeviceIdProvider()
        {
            if (Preferences.ContainsKey(Key))
                DeviceId = Guid.Parse(Preferences.Get(Key, ""));
            else
            {
                DeviceId = Guid.NewGuid();
                Preferences.Set(Key, DeviceId.ToString());
            }
        }
    }
}
