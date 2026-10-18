using System.Net.Http.Json;

using AppDtos = CrepeDuChef.Application.DTOs;
using AppRequests = CrepeDuChef.Application.Models.Requests;
using AppInterfaces = CrepeDuChef.Application.Interfaces;

namespace CrepeDuChef.UI.Shared.Services
{
    public class ChefDataService : AppInterfaces.IChefDataService
    {
        private readonly HttpClient _http;

        public ChefDataService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<AppDtos.UserDto>> GetAllChefsAsync()
        {
            List<AppDtos.UserDto>? result =
                await _http.GetFromJsonAsync<List<AppDtos.UserDto>>("/chefs");

            return result ?? [];
        }

        public async Task<AppDtos.UserDto> AddChefAsync(AppRequests.Chefs.AddChefClientRequest userAddRequest)
        {
            HttpResponseMessage response =
                await _http.PostAsJsonAsync("/chefs", userAddRequest);

            response.EnsureSuccessStatusCode();

            return (await response.Content.ReadFromJsonAsync<AppDtos.UserDto>())!;
        }

        public async Task<AppDtos.UserDto?> UpdateChefAsync(Guid userId, AppRequests.Chefs.UpdateChefClientRequest userUpdateRequest)
        {
            HttpResponseMessage response = 
                await _http.PutAsJsonAsync(
                    $"/chefs/{userId}",
                    new AppRequests.Chefs.UpdateChefClientRequest(
                        userUpdateRequest.FirstName,
                        userUpdateRequest.LastName));

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<AppDtos.UserDto>();
        }
    }
}
