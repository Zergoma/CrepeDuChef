using System.Net.Http.Json;

using AppInterfaces = CrepeDuChef.Application.Interfaces;
using AppDtos = CrepeDuChef.Application.DTOs;
using AppModels = CrepeDuChef.Application.Models;
using AppRequests = CrepeDuChef.Application.Models.Requests;


namespace CrepeDuChef.UI.Shared.Services
{
    public class CrepePartyDataService : AppInterfaces.ICrepePartyDataService
    {
        private readonly HttpClient _http;

        public CrepePartyDataService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<AppModels.CrepePartySession>> GetSessionsAsync()
        {
            return await _http.GetFromJsonAsync<List<AppModels.CrepePartySession>>("api/crepeparty/sessions")
                   ?? [];
        }

        public async Task AddCrepePartyAsync(AppRequests.CrepeParty.AddCrepePartyClientRequest addCrepePartRequest)
        {
            HttpResponseMessage response =
                await _http.PostAsJsonAsync("api/crepeparty", addCrepePartRequest);
            
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<AppDtos.UserDto>> GetAllChefsAsync()
        {
            return await _http.GetFromJsonAsync<List<AppDtos.UserDto>>("api/crepeparty/chefs")
                   ?? [];
        }
    }
}
