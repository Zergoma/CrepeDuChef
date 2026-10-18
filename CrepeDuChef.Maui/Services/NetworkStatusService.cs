using CrepeDuChef.Application.Interfaces;

using Microsoft.Maui.Networking;

namespace CrepeDuChef.Maui.Services
{
    public class NetworkStatusService : INetworkStatusService
    {
        public bool IsOnline()
        {
            return Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
        }
    }
}
