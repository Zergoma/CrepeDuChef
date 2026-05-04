using System;
using System.IO;

using CrepeDuChef.Application.Interfaces;

namespace CrepeDuChef.Avalonia.Services
{
    public class AvaloniaDeviceIdProvider : IDeviceIdProvider
    {
        private const string FileName = "device_id.txt";
        public Guid DeviceId { get; }

        public AvaloniaDeviceIdProvider()
        {
            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "CrepeDuChef",
                FileName);

            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            if (File.Exists(path))
                DeviceId = Guid.Parse(File.ReadAllText(path));
            else
            {
                DeviceId = Guid.NewGuid();
                File.WriteAllText(path, DeviceId.ToString());
            }
        }
    }

}
