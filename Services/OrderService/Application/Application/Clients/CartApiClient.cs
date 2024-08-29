using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Clients
{ 
public class CartApiClient
{
    private readonly HttpClient _httpClient;

    public CartApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CartDto> GetCartByIdAsync(int cartId)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5259/api/cart/{cartId}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CartDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        return null;
    }

    public async Task<CartDto> GetCartByUserIdAsync(int userId)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5259/api/cart/user/{userId}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CartDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        return null;
    }

    public async Task<bool> UpdateCartStatusAsync(int cartId, CartStatusUpdateDto statusUpdateDto)
    {
        var content = new StringContent(JsonSerializer.Serialize(statusUpdateDto), System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"http://localhost:5259/api/cart/{cartId}/status", content);
        return response.IsSuccessStatusCode;
    }
}
}
