using System.Net.Http.Json;

using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.ValueObjects;

namespace CrepeDuChef.UI.Shared.Services
{
    public class ChefRotationDataService : IChefRotationDataService
    {
        private readonly HttpClient _http;

        public ChefRotationDataService(HttpClient http)
        {
            _http = http;
        }
        public async Task<ChefSelectionResult> GetNextChefAsync(List<UserDto>? availableChefs = null)
        {
            var response =
                await _http.PostAsJsonAsync("chefrotation/next", availableChefs);
            
            response.EnsureSuccessStatusCode();

            ChefSelectionResult? result =
                await response.Content.ReadFromJsonAsync<ChefSelectionResult>();
            
            return result ?? throw new InvalidOperationException("Empty response from rotation API");
        }
    }
}
