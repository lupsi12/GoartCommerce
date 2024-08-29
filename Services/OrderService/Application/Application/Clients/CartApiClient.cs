using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
            var response = await _httpClient.GetAsync($"http://localhost:5259/api/api/cart/{cartId}");
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

        public async Task<bool> UpdateCartStatusAsync(int cartId, int userId, CartStatus newStatus, List<CartItemDto> cartItems)
        {
            var updateCartRequest = new
            {
                CartId = cartId,
                UserId = userId,
                Status = newStatus,
                //Items = cartItems.Select(ci => new
                //{
                //    ProductId = ci.ProductId,
                //    Quantity = ci.Quantity
                //}).ToList()
            };

            var content = new StringContent(JsonSerializer.Serialize(updateCartRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"http://localhost:5259/api/cart/update", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<CartDto> GetActiveCartByUserIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5259/api/cart/all?userId={userId}&status=0");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var carts = JsonSerializer.Deserialize<List<CartDto>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return carts?.FirstOrDefault();
            }

            return null;
        }

        public async Task CreateCartAsync(int userId)
        {
            var createCartRequest = new
            {
                UserId = userId
            };

            var content = new StringContent(JsonSerializer.Serialize(createCartRequest), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"http://localhost:5259/api/cart/create", content);
            
        }
    }

    public class CartDto
    {
        [JsonPropertyName("cartId")]
        public int Id { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("status")]
        public CartStatus Status { get; set; }

        [JsonPropertyName("totalPrice")]
        public decimal TotalPrice { get; set; }

        [JsonPropertyName("cartItems")]
        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();

        [JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; }
    }

    public class CartItemDto
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("pricePerUnit")]
        public decimal PricePerUnit { get; set; }

        [JsonPropertyName("subtotal")]
        public decimal Subtotal { get; set; }
    }

    public enum CartStatus
    {
        Active = 0,
        Inactive = 1,
        Ordered = 2,
        Pending = 3,
        Abandoned = 4,
        Cancelled = 5
    }
}
